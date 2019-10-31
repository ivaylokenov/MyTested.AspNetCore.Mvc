namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;

    public class WithoutTempDataBuilder : BaseTempDataBuilder, IAndWithoutTempDataBuilder
    {
        public WithoutTempDataBuilder(ITempDataDictionary tempData)
            : base(tempData)
        {
        }

        public IAndWithoutTempDataBuilder WithoutEntries(IEnumerable<string> entriesKeys)
        {
            foreach (var key in entriesKeys)
            {
                this.WithoutEntry(key);
            }

            return this;
        }

        public IAndWithoutTempDataBuilder WithoutEntry(string key)
        {
            this.TempData.Remove(key);
            return this;
        }

        public IAndWithoutTempDataBuilder WithoutAllEntries()
        {
            this.TempData.Clear();
            return this;
        }

        public IWithoutTempDataBuilder AndAlso()
            => this;
    }
}
