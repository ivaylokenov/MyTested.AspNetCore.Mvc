namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class TempDataBuilder : IAndTempDataBuilder
    {
        private readonly ITempDataDictionary tempData;

        public TempDataBuilder(ITempDataDictionary tempData)
        {
            CommonValidator.CheckForNullReference(tempData, nameof(ITempDataDictionary));
            this.tempData = tempData;
        }

        public IAndTempDataBuilder WithEntry(string key, object value)
        {
            this.tempData.Add(key, value);
            return this;
        }

        public IAndTempDataBuilder WithEntries(IDictionary<string, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        public IAndTempDataBuilder WithEntries(object entries)
            => this.WithEntries(new RouteValueDictionary(entries));

        public ITempDataBuilder AndAlso()
        {
            return this;
        }
    }
}
