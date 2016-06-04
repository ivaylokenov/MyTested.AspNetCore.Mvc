namespace MyTested.Mvc.Internal.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;

    public class ControllerPropertyHelper
    {
        private const string InvalidDelegateErrorMessage = "The {0} property cannot be activated for value of {1} type.";

        private static readonly MethodInfo CallPropertyGetterOpenGenericMethod =
            typeof(ControllerPropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertyGetter));

        private static readonly ConcurrentDictionary<Type, ControllerPropertyHelper> ControllerPropertiesCache =
            new ConcurrentDictionary<Type, ControllerPropertyHelper>();

        private readonly Type controllerType;

        private Func<object, ControllerContext> controllerContextGetter;
        private Func<object, ActionContext> actionContextGetter;

        public ControllerPropertyHelper(Type controllerType)
        {
            this.controllerType = controllerType;
            this.Properties = controllerType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public Func<object, ControllerContext> ControllerContextGetter
        {
            get
            {
                if (this.controllerContextGetter == null)
                {
                    this.TryCreateControllerContextDelegates();
                }

                return this.controllerContextGetter;
            }
        }

        public Func<object, ActionContext> ActionContextGetter
        {
            get
            {
                if (this.actionContextGetter == null)
                {
                    this.TryCreateActionContextDelegates();
                }

                return this.actionContextGetter;
            }
        }

        protected IEnumerable<PropertyInfo> Properties { get; private set; }

        public static ControllerPropertyHelper GetProperties<TController>()
            where TController : class
        {
            return GetProperties(typeof(TController));
        }

        public static ControllerPropertyHelper GetProperties(Type type)
        {
            return ControllerPropertiesCache.GetOrAdd(type, _ => new ControllerPropertyHelper(type));
        }

        protected static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo propertyInfo)
        {
            try
            {
                var propertyGetMethod = propertyInfo.GetMethod;

                var typeInput = propertyGetMethod.DeclaringType;
                var typeOutput = propertyGetMethod.ReturnType;

                var delegateType = typeof(Func<,>).MakeGenericType(typeInput, typeOutput);
                var propertyGetterDelegate = propertyGetMethod.CreateDelegate(delegateType);

                var wrapperDelegateMethod = CallPropertyGetterOpenGenericMethod.MakeGenericMethod(typeInput, typeOutput);
                var accessorDelegate = wrapperDelegateMethod.CreateDelegate(
                    typeof(Func<object, TResult>),
                    propertyGetterDelegate);

                return (Func<object, TResult>)accessorDelegate;
            }
            catch
            {
                throw new InvalidOperationException(string.Format(InvalidDelegateErrorMessage, propertyInfo.Name, typeof(TResult)));
            }
        }

        // Called via reflection
        private static TValue CallPropertyGetter<TDeclaringType, TValue>(
            Func<TDeclaringType, TValue> getter,
            object target)
        {
            return getter((TDeclaringType)target);
        }

        private void TryCreateControllerContextDelegates()
        {
            var controllerContextProperty = this.FindPropertyWithAttribute<ControllerContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(controllerContextProperty, nameof(ControllerContext));

            this.controllerContextGetter = MakeFastPropertyGetter<ControllerContext>(controllerContextProperty);
        }

        private void TryCreateActionContextDelegates()
        {
            var actionContextProperty = this.FindPropertyWithAttribute<ActionContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(actionContextProperty, nameof(ActionContext));

            this.actionContextGetter = MakeFastPropertyGetter<ActionContext>(actionContextProperty);
        }

        protected PropertyInfo FindPropertyWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return this.Properties.FirstOrDefault(pr => pr.GetCustomAttribute(typeof(TAttribute), true) != null);
        }

        protected void ThrowNewInvalidOperationExceptionIfNull(object value, string propertyName)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{propertyName} could not be found on the provided {controllerType.ToFriendlyTypeName()}. The property should be specified manually by providing controller instance or using the specified helper methods.");
            }
        }
    }
}
