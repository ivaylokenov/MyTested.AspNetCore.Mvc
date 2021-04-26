namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Builders.Contracts.Data;
    using Builders.Data.DistributedCache;
    using Contracts.Data.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Utilities.Extensions;

    /// <inheritdoc />
    public class WithDistributedCacheBuilder : BaseDistributedCacheBuilder, IAndWithDistributedCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithDistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public WithDistributedCacheBuilder(IServiceProvider services)
            : base(services)
        {
        }

        /// <inheritdoc />
        public IAndWithDistributedCacheBuilder WithEntry(string key, byte[] value)
        {
            this.DistributedCache.Set(key, value);
            return this;
        }

        public IAndWithDistributedCacheBuilder WithEntry(string key, string value)
        {
            this.DistributedCache.SetString(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithDistributedCacheBuilder WithEntry(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            this.DistributedCache.Set(key, value, options);
            return this;
        }

        public IAndWithDistributedCacheBuilder WithEntry(string key, string value, DistributedCacheEntryOptions options)
        {
            this.DistributedCache.SetString(key, value, options);
            return this;
        }

        public IAndWithDistributedCacheBuilder WithEntry(Action<IDistributedCacheEntryKeyBuilder> distributedCacheEntryBuilder)
        {
            var newDistributedCacheEntryBuilder = new DistributedCacheEntryBuilder();
            distributedCacheEntryBuilder(newDistributedCacheEntryBuilder);

            var distributedCacheKey = newDistributedCacheEntryBuilder.EntryKey;
            var distributedCacheEntry = newDistributedCacheEntryBuilder.DistributedCacheEntry;

            return this.WithEntry(distributedCacheKey, distributedCacheEntry.Value, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = distributedCacheEntry.Options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = distributedCacheEntry.Options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = distributedCacheEntry.Options.SlidingExpiration
            });
        }

        /// <inheritdoc />
        public IAndWithDistributedCacheBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        public IAndWithDistributedCacheBuilder WithEntries(IDictionary<string, string> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IWithDistributedCacheBuilder AndAlso() => this;
    }
}
