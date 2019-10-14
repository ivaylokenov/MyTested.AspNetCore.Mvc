namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.Extensions.Caching.Memory;
    using Utilities.Extensions;

    /// <inheritdoc />
    public class WithoutMemoryCacheBuilder : BaseMemoryCacheBuilder, IAndWithoutMemoryCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutMemoryCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IMemoryCache"/>.</param>
        public WithoutMemoryCacheBuilder(IServiceProvider services) 
            : base(services)
        {
        }

        /// <inheritdoc />
        public IAndWithoutMemoryCacheBuilder WithoutEntry(object key)
        {
            this.MemoryCache.Remove(key);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutMemoryCacheBuilder WithoutEntries(IEnumerable<object> keys)
        {
            this.MemoryCache.AsMemoryCacheMock().RemoveKeys(keys);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutMemoryCacheBuilder WithoutAllEntries()
        {
            this.MemoryCache.AsMemoryCacheMock().ClearCache();
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutMemoryCacheBuilder WithoutEntries(params object[] keys)
        {
            this.MemoryCache.AsMemoryCacheMock().RemoveKeys(keys);
            return this;
        }

        /// <inheritdoc />
        public IWithoutMemoryCacheBuilder AndAlso() => this;
    }
}
