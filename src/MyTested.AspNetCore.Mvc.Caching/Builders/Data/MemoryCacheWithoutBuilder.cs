namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Utilities.Extensions;

    public class MemoryCacheWithoutBuilder : MemoryCacheBaseBuilder, IAndWithoutMemoryCacheBuilder
    {
        public MemoryCacheWithoutBuilder(IServiceProvider services) : base(services) { }

        public IAndWithoutMemoryCacheBuilder WithoutEntry(object key)
        {
            this.MemoryCache.Remove(key);
            return this;
        }

        public IAndWithoutMemoryCacheBuilder WithoutEntries(IEnumerable<object> keys)
        {
            this.MemoryCache.AsMemoryCacheMock().RemoveKeys(keys);
            return this;
        }

        public IAndWithoutMemoryCacheBuilder ClearCache()
        {
            this.MemoryCache.AsMemoryCacheMock().ClearCache();
            return this;
        }

        public IWithoutMemoryCacheBuilder AndAlso() => this;
    }
}
