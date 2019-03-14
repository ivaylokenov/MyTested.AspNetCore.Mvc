namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class TempDataPropertyHelper : PropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, TempDataPropertyHelper> TempDataPropertiesCache =
            new ConcurrentDictionary<Type, TempDataPropertyHelper>();

        private static readonly TypeInfo TypeOfTempDataDictionary = typeof(ITempDataDictionary).GetTypeInfo();

        private Func<object, ITempDataDictionary> tempDataGetter;

        public TempDataPropertyHelper(Type type)
            : base(type)
        {
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

        public static TempDataPropertyHelper GetTempDataProperties<TController>()
            where TController : class 
            => GetTempDataProperties(typeof(TController));

        public static TempDataPropertyHelper GetTempDataProperties(Type type) 
            => TempDataPropertiesCache.GetOrAdd(type, _ => new TempDataPropertyHelper(type));

        private void TryCreateTempDataGetterDelegate()
        {
            var tempDataProperty = this.Properties.FirstOrDefault(pr => TypeOfTempDataDictionary.IsAssignableFrom(pr.PropertyType));
            this.ThrowNewInvalidOperationExceptionIfNull(tempDataProperty, nameof(TempDataDictionary));

            this.tempDataGetter = MakeFastPropertyGetter<ITempDataDictionary>(tempDataProperty);
        }
    }
}
