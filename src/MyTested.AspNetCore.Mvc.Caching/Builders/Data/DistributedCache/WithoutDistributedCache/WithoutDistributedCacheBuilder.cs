namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache.WithoutDistributedCache
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;
    using MyTested.AspNetCore.Mvc.Utilities.Extensions;

    /// <inheritdoc />
    public class WithoutDistributedCacheBuilder : BaseDistributedCacheBuilder, IAndWithoutDistributedCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutDistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public WithoutDistributedCacheBuilder(IServiceProvider services)
            : base(services)
        {
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCacheBuilder WithoutAllEntries()
        {
            this.DistributedCache.AsDistributedCacheMock().ClearCache();
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCacheBuilder WithoutEntries(IEnumerable<string> keys)
        {
            this.DistributedCache.AsDistributedCacheMock().RemoveKeys(keys);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCacheBuilder WithoutEntries(params string[] keys)
        {
            this.DistributedCache.AsDistributedCacheMock().RemoveKeys(keys);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCacheBuilder WithoutEntry(string key)
        {
            this.DistributedCache.Remove(key);
            return this;
        }

        /// <inheritdoc />
        public IWithoutDistributedCacheBuilder AndAlso() => this;
    }
}
