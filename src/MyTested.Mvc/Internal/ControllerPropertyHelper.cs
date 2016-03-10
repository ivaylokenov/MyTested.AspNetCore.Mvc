namespace MyTested.Mvc.Internal
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Utilities;

    public class ControllerPropertyHelper
    {
        private const string InvalidDelegateErrorMessage = "The {0} property cannot be activated for value of {1} type.";

        private static readonly ConcurrentDictionary<Type, ControllerPropertyHelper> controllerPropertiesCache =
            new ConcurrentDictionary<Type, ControllerPropertyHelper>();

        private static readonly MethodInfo CallPropertyGetterOpenGenericMethod =
            typeof(ControllerPropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertyGetter));

        private static readonly MethodInfo CallPropertySetterOpenGenericMethod =
            typeof(ControllerPropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertySetter));

        private readonly Type controllerType;
        private readonly IEnumerable<PropertyInfo> properties;

        private Func<object, ControllerContext> controllerContextGetter;
        private Action<object, ControllerContext> controllerContextSetter;
        private Func<object, ActionContext> actionContextGetter;
        private Action<object, ActionContext> actionContextSetter;
        private Func<object, ViewDataDictionary> viewDataGetter;
        private Func<object, ITempDataDictionary> tempDataGetter;

        public ControllerPropertyHelper(Type controllerType)
        {
            this.controllerType = controllerType;
            this.properties = controllerType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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

        public Action<object, ControllerContext> ControllerContextSetter
        {
            get
            {
                if (this.controllerContextSetter == null)
                {
                    this.TryCreateControllerContextDelegates();
                }

                return this.controllerContextSetter;
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

        public Action<object, ActionContext> ActionContextSetter
        {
            get
            {
                if (this.actionContextSetter == null)
                {
                    this.TryCreateActionContextDelegates();
                }

                return this.actionContextSetter;
            }
        }

        public Func<object, ViewDataDictionary> ViewDataGetter
        {
            get
            {
                if (this.viewDataGetter == null)
                {
                    this.TryCreateViewDataGetterDelegate();
                }

                return this.viewDataGetter;
            }
        }

        public Func<object, ITempDataDictionary> TempDataGetter
        {
            get
            {
                if (this.tempDataGetter == null)
                {
                    this.TryCreateTempDataGetterDelegate();
                }

                return this.tempDataGetter;
            }
        }

        public static ControllerPropertyHelper GetProperties<TController>()
            where TController : class
        {
            return GetProperties(typeof(TController));
        }

        public static ControllerPropertyHelper GetProperties(Type type)
        {
            return controllerPropertiesCache.GetOrAdd(type, _ =>
            {
                return new ControllerPropertyHelper(type);
            });
        }
        
        private static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo propertyInfo)
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

        private static Action<object, TValue> MakeFastPropertySetter<TValue>(PropertyInfo propertyInfo)
        {
            try
            {
                var setMethod = propertyInfo.SetMethod;
                var parameters = setMethod.GetParameters();

                var typeInput = setMethod.DeclaringType;
                var parameterType = parameters[0].ParameterType;

                // Create a delegate TDeclaringType -> { TDeclaringType.Property = TValue; }
                var propertySetterAsAction =
                    setMethod.CreateDelegate(typeof(Action<,>).MakeGenericType(typeInput, parameterType));
                var callPropertySetterClosedGenericMethod =
                    CallPropertySetterOpenGenericMethod.MakeGenericMethod(typeInput, parameterType);
                var callPropertySetterDelegate =
                    callPropertySetterClosedGenericMethod.CreateDelegate(
                        typeof(Action<object, TValue>), propertySetterAsAction);

                return (Action<object, TValue>)callPropertySetterDelegate;
            }
            catch
            {
                throw new InvalidOperationException(string.Format(InvalidDelegateErrorMessage, propertyInfo.Name, typeof(TValue)));
            }
        }

        // Called via reflection
        private static TValue CallPropertyGetter<TDeclaringType, TValue>(
            Func<TDeclaringType, TValue> getter,
            object target)
        {
            return getter((TDeclaringType)target);
        }

        // Called via reflection
        private static void CallPropertySetter<TDeclaringType, TValue>(
            Action<TDeclaringType, TValue> setter,
            object target,
            object value)
        {
            setter((TDeclaringType)target, (TValue)value);
        }
        
        private void TryCreateControllerContextDelegates()
        {
            var controllerContextProperty = this.FindPropertyWithAttribute<ControllerContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(controllerContextProperty, nameof(ControllerContext));

            this.controllerContextGetter = MakeFastPropertyGetter<ControllerContext>(controllerContextProperty);
            this.controllerContextSetter = MakeFastPropertySetter<ControllerContext>(controllerContextProperty);
        }

        private void TryCreateActionContextDelegates()
        {
            var actionContextProperty = this.FindPropertyWithAttribute<ActionContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(actionContextProperty, nameof(ActionContext));

            this.actionContextGetter = MakeFastPropertyGetter<ActionContext>(actionContextProperty);
            this.actionContextSetter = MakeFastPropertySetter<ActionContext>(actionContextProperty);
        }

        private void TryCreateViewDataGetterDelegate()
        {
            var viewDataProperty = this.FindPropertyWithAttribute<ViewDataDictionaryAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(viewDataProperty, nameof(ViewDataDictionary));

            this.viewDataGetter = MakeFastPropertyGetter<ViewDataDictionary>(viewDataProperty);
        }

        private void TryCreateTempDataGetterDelegate()
        {
            var tempDataProperty = this.properties.FirstOrDefault(pr => typeof(ITempDataDictionary).IsAssignableFrom(pr.PropertyType));
            this.ThrowNewInvalidOperationExceptionIfNull(tempDataProperty, nameof(TempDataDictionary));

            this.tempDataGetter = MakeFastPropertyGetter<ITempDataDictionary>(tempDataProperty);
        }

        private PropertyInfo FindPropertyWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return this.properties.FirstOrDefault(pr => pr.GetCustomAttribute(typeof(TAttribute), true) != null);
        }

        private void ThrowNewInvalidOperationExceptionIfNull(object value, string propertyName)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{propertyName} could not be found on the provided {controllerType.ToFriendlyTypeName()}. The property should be specified manually by providing controller instance or using the specified helper methods.");
            }
        }
    }
}
