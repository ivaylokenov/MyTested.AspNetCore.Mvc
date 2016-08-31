namespace MyTested.AspNetCore.Mvc.Internal.ViewFeatures
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class ViewDataPropertyHelper : PropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, ViewDataPropertyHelper> ViewDataPropertiesCache =
            new ConcurrentDictionary<Type, ViewDataPropertyHelper>();

        private static readonly TypeInfo TypeOfViewDataDictionary = typeof(ViewDataDictionary).GetTypeInfo();

        private Func<object, ViewDataDictionary> viewDataGetter;

        public ViewDataPropertyHelper(Type componentType)
            : base(componentType)
        {
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

        public static ViewDataPropertyHelper GetViewDataProperties<TController>()
            where TController : class
        {
            return GetViewDataProperties(typeof(TController));
        }

        public static ViewDataPropertyHelper GetViewDataProperties(Type type)
        {
            return ViewDataPropertiesCache.GetOrAdd(type, _ => new ViewDataPropertyHelper(type));
        }

        private void TryCreateViewDataGetterDelegate()
        {
            var viewDataProperty = 
                this.FindPropertyWithAttribute<ViewDataDictionaryAttribute>() ??
                this.Properties.FirstOrDefault(pr => TypeOfViewDataDictionary.IsAssignableFrom(pr.PropertyType)); ;

            this.ThrowNewInvalidOperationExceptionIfNull(viewDataProperty, nameof(ViewDataDictionary));

            this.viewDataGetter = MakeFastPropertyGetter<ViewDataDictionary>(viewDataProperty);
        }
    }
}
