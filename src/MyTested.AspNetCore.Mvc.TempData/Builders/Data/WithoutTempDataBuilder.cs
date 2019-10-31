
namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;

    public class WithoutTempDataBuilder : BaseTempDataBuilder, IAndWithoutTempDataBuilder
    {
        public WithoutTempDataBuilder(ITempDataDictionary tempData)
            : base(tempData)
        {
        }

        public IWithoutTempDataBuilder AndAlso()
        {
            throw new NotImplementedException();
        }

        public IAndWithoutTempDataBuilder WithEntries(IDictionary<string, object> entries)
        {
            throw new NotImplementedException();
        }

        public IAndWithoutTempDataBuilder WithEntries(object entries)
        {
            throw new NotImplementedException();
        }

        public IAndWithoutTempDataBuilder WithEntry(string key, object value)
        {
            throw new NotImplementedException();
        }

        public IAndWithoutTempDataBuilder WithoutAllEntries()
        {
            throw new NotImplementedException();
        }
    }
}
