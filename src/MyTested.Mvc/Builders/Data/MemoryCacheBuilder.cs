namespace MyTested.Mvc.Builders.Data
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Contracts.Data;

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

        public IMemoryCacheBuilder AndAlso()
        {
            return this;
        }
    }
}
