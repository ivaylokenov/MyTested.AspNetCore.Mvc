namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;

    public class ViewDataTestBuilder : BaseDataProviderTestBuilder, IAndViewDataTestBuilder
    {
        internal const string ViewDataName = "view data";

        public ViewDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewDataName)
        {
        }

        public IAndViewDataTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntryOfType<TEntry>()
        {
            this.ValidateContainingEntryOfType<TEntry>();
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntryOfType<TEntry>(string key)
        {
            this.ValidateContainingEntryOfType<TEntry>(key);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        public IAndViewDataTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this;
        }

        public IViewDataTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.ViewData;
    }
}
