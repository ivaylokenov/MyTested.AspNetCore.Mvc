namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.Extensions.Caching.Memory;
    using Contracts.Data.MemoryCache;
    using Data.MemoryCache;
    using Utilities.Extensions;

    /// <inheritdoc />
    public class WithMemoryCacheBuilder : BaseMemoryCacheBuilder, IAndWithMemoryCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithMemoryCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IMemoryCache"/>.</param>
        public WithMemoryCacheBuilder(IServiceProvider services)
            : base(services)
        {
        }

        /// <inheritdoc />
        public IAndWithMemoryCacheBuilder WithEntry(object key, object value)
        {
            this.MemoryCache.Set(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithMemoryCacheBuilder WithEntry(object key, object value, MemoryCacheEntryOptions options)
        {
            this.MemoryCache.Set(key, value, options);
            return this;
        }

        /// <inheritdoc />
        public IAndWithMemoryCacheBuilder WithEntry(Action<IMemoryCacheEntryKeyBuilder> memoryCacheEntryBuilder)
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
        public IAndWithMemoryCacheBuilder WithEntries(IDictionary<object, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IWithMemoryCacheBuilder AndAlso() => this;
    }
}
