namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;

    public class ViewBagTestBuilder : BaseDataProviderTestBuilder, IAndViewBagTestBuilder
    {
        private const string ViewBagName = "view bag";

        public ViewBagTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewBagName)
        {
        }

        public IAndViewBagTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this;
        }

        public IAndViewBagTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this;
        }

        public IAndViewBagTestBuilder ContainingEntryOfType<TEntry>()
        {
            this.ValidateContainingEntryOfType<TEntry>();
            return this;
        }

        public IAndViewBagTestBuilder ContainingEntryOfType<TEntry>(string key)
        {
            this.ValidateContainingEntryOfType<TEntry>(key);
            return this;
        }

        public IAndViewBagTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndViewBagTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        public IAndViewBagTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this;
        }

        public IViewBagTestBuilder AndAlso()
        {
            return this;
        }

        protected override IDictionary<string, object> GetDataProvider()
        {
            return this.TestContext.ControllerAs<Controller>().ViewData;
        }
    }
}
