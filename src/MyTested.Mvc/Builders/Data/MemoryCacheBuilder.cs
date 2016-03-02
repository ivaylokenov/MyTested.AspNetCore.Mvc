namespace MyTested.Mvc.Builders.Data
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Contracts.Data;
    using System.Collections.Generic;
    using Utilities.Extensions;

    public class MemoryCacheBuilder : IAndMemoryCacheBuilder
    {
        public MemoryCacheBuilder(IServiceProvider services)
        {
            this.MemoryCache = services.GetService<IMemoryCache>();
        }

        protected IMemoryCache MemoryCache { get; private set; }
        
        public IAndMemoryCacheBuilder WithEntry(object key, object value)
        {
            this.MemoryCache.Set(key, value);
            return this;
        }

        public IAndMemoryCacheBuilder WithEntry(object key, object value, MemoryCacheEntryOptions options)
        {
            this.MemoryCache.Set(key, value, options);
            return this;
        }

        public IAndMemoryCacheBuilder WithEntry(Action<IMemoryCacheEntryTestBuilder> memoryCacheEntryBuilder)
        {
            var newMemoryCacheEntryBuilder = new MemoryCacheEntryBuilder();
            memoryCacheEntryBuilder(newMemoryCacheEntryBuilder);
            var memoryCacheEntry = newMemoryCacheEntryBuilder.GetMockedMemoryCacheEntry();

            return this.WithEntry(memoryCacheEntry.Key, memoryCacheEntry.Value, memoryCacheEntry.Options);
        }

        public IAndMemoryCacheBuilder WithEntries(IDictionary<object, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        public IMemoryCacheBuilder AndAlso() => this;
    }
}
