namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public class DistributedCacheBuilder : IAndDistributedCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public DistributedCacheBuilder(IServiceProvider services)
            => this.DistributedCache = services.GetRequiredService<IDistributedCache>();

        /// <summary>
        /// Gets the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <value>Built <see cref="IDistributedCache"/>.</value>
        protected IDistributedCache DistributedCache { get; private set; }

        /// <inheritdoc />
        public IAndDistributedCacheBuilder WithEntry(string key, byte[] value)
        {
            this.DistributedCache.Set(key, value);
            return this;
        }

        public IAndDistributedCacheBuilder WithEntry(string key, string value)
        {
            this.DistributedCache.SetString(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheBuilder WithEntry(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            this.DistributedCache.Set(key, value, options);
            return this;
        }

        public IAndDistributedCacheBuilder WithEntry(string key, string value, DistributedCacheEntryOptions options)
        {
            this.DistributedCache.SetString(key, value, options);
            return this;
        }

        public IAndDistributedCacheBuilder WithEntry(Action<IDistributedCacheEntryKeyBuilder> distributedCacheEntryBuilder)
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
        public IAndDistributedCacheBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        public IAndDistributedCacheBuilder WithEntries(IDictionary<string, string> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IDistributedCacheBuilder AndAlso() => this;
    }
}
