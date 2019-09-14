namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
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
        {
            this.DistributedCache = services.GetRequiredService<IDistributedCache>();
        }

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

        /// <inheritdoc />
        public IAndDistributedCacheBuilder WithEntry(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            this.DistributedCache.Set(key, value, options);
            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IDistributedCacheBuilder AndAlso() => this;
    }
}
