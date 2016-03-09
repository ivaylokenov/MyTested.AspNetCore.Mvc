namespace MyTested.Mvc.Internal
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public class ControllerPropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, ControllerPropertyHelper> controllerPropertiesCache =
            new ConcurrentDictionary<Type, ControllerPropertyHelper>();

        private static readonly MethodInfo CallPropertyGetterOpenGenericMethod =
            typeof(ControllerPropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertyGetter));

        private static readonly MethodInfo CallPropertySetterOpenGenericMethod =
            typeof(ControllerPropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertySetter));

        private readonly IEnumerable<PropertyInfo> properties;

        private Func<object, ControllerContext> controllerContextGetter;
        private Action<object, ControllerContext> controllerContextSetter;
        private Func<object, ViewDataDictionary> viewDataGetter;
        private Func<object, ITempDataDictionary> tempDataGetter;

        public ControllerPropertyHelper(Type controllerType)
        {
            this.properties = controllerType.GetTypeInfo().DeclaredProperties;
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

        public static ControllerPropertyHelper GetProperties(Type type)
        {
            return controllerPropertiesCache.GetOrAdd(type, _ =>
            {
                return new ControllerPropertyHelper(type);
            });
        }

        private static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo propertyInfo)
        {
            return MakeFastPropertyGetter<TResult>(
                propertyInfo,
                CallPropertyGetterOpenGenericMethod);
        }

        private static Func<object, TResult> MakeFastPropertyGetter<TResult>(
            PropertyInfo propertyInfo,
            MethodInfo propertyGetterWrapperMethod)
        {
            var getMethod = propertyInfo.GetMethod;

            // Create a delegate TDeclaringType -> TValue
            return MakeFastPropertyGetter<TResult>(
                typeof(Func<,>),
                getMethod,
                propertyGetterWrapperMethod);
        }

        private static Func<object, TResult> MakeFastPropertyGetter<TResult>(
            Type openGenericDelegateType,
            MethodInfo propertyGetMethod,
            MethodInfo openGenericWrapperMethod)
        {
            var typeInput = propertyGetMethod.DeclaringType;
            var typeOutput = propertyGetMethod.ReturnType;

            var delegateType = openGenericDelegateType.MakeGenericType(typeInput, typeOutput);
            var propertyGetterDelegate = propertyGetMethod.CreateDelegate(delegateType);

            var wrapperDelegateMethod = openGenericWrapperMethod.MakeGenericMethod(typeInput, typeOutput);
            var accessorDelegate = wrapperDelegateMethod.CreateDelegate(
                typeof(Func<object, TResult>),
                propertyGetterDelegate);

            return (Func<object, TResult>)accessorDelegate;
        }

        private static Action<object, TValue> MakeFastPropertySetter<TValue>(PropertyInfo propertyInfo)
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

        // Called via reflection
        private static object CallPropertyGetter<TDeclaringType, TValue>(
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
            this.ThrowNewInvalidOperationExceptionIfNull(controllerContextProperty);

            this.controllerContextGetter = MakeFastPropertyGetter<ControllerContext>(controllerContextProperty);
            this.controllerContextSetter = MakeFastPropertySetter<ControllerContext>(controllerContextProperty);
        }

        private void TryCreateViewDataGetterDelegate()
        {
            var viewDataProperty = this.FindPropertyWithAttribute<ViewDataDictionaryAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(viewDataProperty);

            this.viewDataGetter = MakeFastPropertyGetter<ViewDataDictionary>(viewDataProperty);
        }

        private void TryCreateTempDataGetterDelegate()
        {
            var tempDataProperty = this.properties.FirstOrDefault(pr => pr.PropertyType == typeof(ITempDataDictionary));
            this.ThrowNewInvalidOperationExceptionIfNull(tempDataProperty);

            this.tempDataGetter = MakeFastPropertyGetter<ITempDataDictionary>(tempDataProperty);
        }

        private PropertyInfo FindPropertyWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return this.properties.FirstOrDefault(pr => pr.GetCustomAttribute(typeof(TAttribute), true) != null);
        }

        private void ThrowNewInvalidOperationExceptionIfNull(object value)
        {
            if (value == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
