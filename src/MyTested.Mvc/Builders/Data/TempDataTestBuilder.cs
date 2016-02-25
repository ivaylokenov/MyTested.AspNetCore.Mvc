namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;

    public class TempDataTestBuilder : BaseDataProviderTestBuilder, IAndTempDataTestBuilder
    {
        private const string ViewDataName = "temp data";

        public TempDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewDataName)
        {
        }

        public IAndTempDataTestBuilder ContainingEntry(string key)
        {
            this.ValidateContainingEntry(key);
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntry<TEntry>(TEntry value)
        {
            this.ValidateContainingEntry(value);
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntryOfType<TEntry>()
        {
            this.ValidateContainingEntryOfType<TEntry>();
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntryOfType<TEntry>(string key)
        {
            this.ValidateContainingEntryOfType<TEntry>(key);
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));
        
        public IAndTempDataTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this;
        }

        public ITempDataTestBuilder AndAlso()
        {
            return this;
        }

        protected override IDictionary<string, object> GetDataProvider()
        {
            return this.TestContext.ControllerAs<Controller>().ViewData;
        }
    }
}
