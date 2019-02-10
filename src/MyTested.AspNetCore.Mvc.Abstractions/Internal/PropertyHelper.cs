namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Utilities;

    public abstract class PropertyHelper
    {
        private const string InvalidDelegateErrorMessage = "The {0} property cannot be activated for value of {1} type.";

        private static readonly MethodInfo CallPropertyGetterOpenGenericMethod =
            typeof(PropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertyGetter));
        
        private static readonly MethodInfo CallPropertySetterOpenGenericMethod =
            typeof(PropertyHelper).GetTypeInfo().GetDeclaredMethod(nameof(CallPropertySetter));

        protected PropertyHelper(Type type)
        {
            this.Type = type;
            this.Properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        protected Type Type { get; }

        protected IEnumerable<PropertyInfo> Properties { get; }
        
        public static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo propertyInfo)
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

        public static Action<object, object> MakeFastPropertySetter(PropertyInfo propertyInfo)
        {
            var setMethod = propertyInfo.SetMethod;
            var parameters = setMethod.GetParameters();
            
            var typeInput = setMethod.DeclaringType;
            var parameterType = parameters[0].ParameterType;
            
            var propertySetterAsAction =
                setMethod.CreateDelegate(typeof(Action<,>).MakeGenericType(typeInput, parameterType));
            var callPropertySetterClosedGenericMethod =
                CallPropertySetterOpenGenericMethod.MakeGenericMethod(typeInput, parameterType);
            var callPropertySetterDelegate =
                callPropertySetterClosedGenericMethod.CreateDelegate(
                    typeof(Action<object, object>), propertySetterAsAction);

            return (Action<object, object>)callPropertySetterDelegate;
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
                throw new InvalidOperationException($"{propertyName} could not be found on the provided {this.Type.ToFriendlyTypeName()}. The property should be specified manually by providing component instance or using the specified helper methods.");
            }
        }

        // Called via reflection.
        private static TValue CallPropertyGetter<TDeclaringType, TValue>(
            Func<TDeclaringType, TValue> getter,
            object target)
        {
            return getter((TDeclaringType)target);
        }

        // Called via reflection.
        private static void CallPropertySetter<TDeclaringType, TValue>(
            Action<TDeclaringType, TValue> setter,
            object target,
            object value)
        {
            setter((TDeclaringType)target, (TValue)value);
        }
    }
}
