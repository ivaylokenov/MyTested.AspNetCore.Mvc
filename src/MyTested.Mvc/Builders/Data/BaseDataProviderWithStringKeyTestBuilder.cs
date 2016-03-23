namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;

    public abstract class BaseDataProviderWithStringKeyTestBuilder<TDataProviderTestBuilder> : BaseDataProviderTestBuilder
        where TDataProviderTestBuilder : class
    {
        protected BaseDataProviderWithStringKeyTestBuilder(ControllerTestContext testContext, string dataProviderName)
            : base(testContext, dataProviderName)
        {
        }


        public TDataProviderTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this.DataProviderTestBuilder;
        }

        public TDataProviderTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this.DataProviderTestBuilder;
        }

        public TDataProviderTestBuilder ContainingEntryOfType<TEntry>()
        {
            this.ValidateContainingEntryOfType<TEntry>();
            return this.DataProviderTestBuilder;
        }

        public TDataProviderTestBuilder ContainingEntryOfType<TEntry>(string key)
        {
            this.ValidateContainingEntryOfType<TEntry>(key);
            return this.DataProviderTestBuilder;
        }

        public TDataProviderTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this.DataProviderTestBuilder;
        }

        public TDataProviderTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        public TDataProviderTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this.DataProviderTestBuilder;
        }

        protected abstract TDataProviderTestBuilder DataProviderTestBuilder { get; }
    }
}
