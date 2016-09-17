namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/>.
    /// </summary>
    public class MemoryCacheBuilder : IAndMemoryCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IMemoryCache"/>.</param>
        public MemoryCacheBuilder(IServiceProvider services)
        {
            this.MemoryCache = services.GetRequiredService<IMemoryCache>();
        }

        /// <summary>
        /// Gets the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <value>Built <see cref="IMemoryCache"/>.</value>
        protected IMemoryCache MemoryCache { get; private set; }

        /// <inheritdoc />
        public IAndMemoryCacheBuilder WithEntry(object key, object value)
        {
            this.MemoryCache.Set(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheBuilder WithEntry(object key, object value, MemoryCacheEntryOptions options)
        {
            this.MemoryCache.Set(key, value, options);
            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheBuilder WithEntry(Action<IMemoryCacheEntryKeyBuilder> memoryCacheEntryBuilder)
        {
            var newMemoryCacheEntryBuilder = new MemoryCacheEntryBuilder();
            memoryCacheEntryBuilder(newMemoryCacheEntryBuilder);
            var memoryCacheEntry = newMemoryCacheEntryBuilder.GetMemoryCacheEntryMock();

            return this.WithEntry(memoryCacheEntry.Key, memoryCacheEntry.Value, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = memoryCacheEntry.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = memoryCacheEntry.AbsoluteExpirationRelativeToNow,
                Priority = memoryCacheEntry.Priority,
                SlidingExpiration = memoryCacheEntry.SlidingExpiration
            });
        }

        /// <inheritdoc />
        public IAndMemoryCacheBuilder WithEntries(IDictionary<object, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IMemoryCacheBuilder AndAlso() => this;
    }
}
