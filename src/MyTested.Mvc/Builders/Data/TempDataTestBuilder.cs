namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;

    public class TempDataTestBuilder : BaseDataProviderTestBuilder, IAndTempDataTestBuilder
    {
        internal const string TempDataName = "temp data";

        public TempDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, TempDataName)
        {
        }

        public IAndTempDataTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this;
        }

        public IAndTempDataTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            this.ValidateContainingEntryWithValue(value);
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

        public ITempDataTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.TempData;
    }
}
