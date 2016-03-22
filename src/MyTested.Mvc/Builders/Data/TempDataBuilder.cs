namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class TempDataBuilder : IAndTempDataBuilder
    {
        public TempDataBuilder(ITempDataDictionary tempData)
        {
            CommonValidator.CheckForNullReference(tempData, nameof(ITempDataDictionary));
            this.TempData = tempData;
        }

        protected ITempDataDictionary TempData { get; private set; }

        public IAndTempDataBuilder WithEntry(string key, object value)
        {
            this.TempData.Add(key, value);
            return this;
        }

        public IAndTempDataBuilder WithEntries(IDictionary<string, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        public IAndTempDataBuilder WithEntries(object entries)
            => this.WithEntries(new RouteValueDictionary(entries));

        public ITempDataBuilder AndAlso() => this;
    }
}
