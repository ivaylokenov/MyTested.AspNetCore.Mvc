namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Utilities.Validators;

    public class PropertyActivator<TContext>
    {
        private readonly Func<TContext, object> valueAccessor;
        private readonly Action<object, object> fastPropertySetter;

        public PropertyActivator(
            PropertyInfo propertyInfo,
            Func<TContext, object> valueAccessor)
        {
            CommonValidator.CheckForNullReference(propertyInfo, nameof(propertyInfo));
            CommonValidator.CheckForNullReference(valueAccessor, nameof(valueAccessor));

            this.PropertyInfo = propertyInfo;
            this.valueAccessor = valueAccessor;
            this.fastPropertySetter = PropertyHelper.MakeFastPropertySetter(propertyInfo);
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public object Activate(object instance, TContext context)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var value = this.valueAccessor(context);
            this.fastPropertySetter(instance, value);
            return value;
        }

        public static PropertyActivator<TContext>[] GetPropertiesToActivate(
            Type type,
            Type activateAttributeType,
            Func<PropertyInfo, PropertyActivator<TContext>> createActivateInfo)
        {
            CommonValidator.CheckForNullReference(type, nameof(type));
            CommonValidator.CheckForNullReference(activateAttributeType, nameof(activateAttributeType));
            CommonValidator.CheckForNullReference(createActivateInfo, nameof(createActivateInfo));

            return type.GetRuntimeProperties()
                .Where(property => property.IsDefined(activateAttributeType)
                    && property.GetIndexParameters().Length == 0
                    && property.SetMethod != null
                    && property.SetMethod.IsPublic
                    && !property.SetMethod.IsStatic)
                .Select(createActivateInfo)
                .ToArray();
        }
    }
}
