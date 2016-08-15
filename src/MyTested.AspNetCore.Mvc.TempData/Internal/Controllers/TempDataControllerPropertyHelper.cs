namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class TempDataControllerPropertyHelper : ControllerPropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, TempDataControllerPropertyHelper> TempDataControllerPropertiesCache =
            new ConcurrentDictionary<Type, TempDataControllerPropertyHelper>();

        private static readonly TypeInfo TypeOfTempDataDictionary = typeof(ITempDataDictionary).GetTypeInfo();

        private Func<object, ITempDataDictionary> tempDataGetter;

        public TempDataControllerPropertyHelper(Type controllerType)
            : base(controllerType)
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

        public static TempDataControllerPropertyHelper GetTempDataProperties<TController>()
            where TController : class
        {
            return GetTempDataProperties(typeof(TController));
        }

        public static TempDataControllerPropertyHelper GetTempDataProperties(Type type)
        {
            return TempDataControllerPropertiesCache.GetOrAdd(type, _ => new TempDataControllerPropertyHelper(type));
        }

        private void TryCreateTempDataGetterDelegate()
        {
            var tempDataProperty = this.Properties.FirstOrDefault(pr => TypeOfTempDataDictionary.IsAssignableFrom(pr.PropertyType));
            this.ThrowNewInvalidOperationExceptionIfNull(tempDataProperty, nameof(TempDataDictionary));

            this.tempDataGetter = MakeFastPropertyGetter<ITempDataDictionary>(tempDataProperty);
        }
    }
}
