namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Contracts.Data;
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/> entry.
    /// </summary>
    public class MemoryCacheEntryBuilder : IMemoryCacheEntryKeyBuilder, IAndMemoryCacheEntryBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheEntryBuilder"/> class.
        /// </summary>
        public MemoryCacheEntryBuilder()
        {
            this.MemoryCacheEntry = new MockedCacheEntry();
        }

        /// <summary>
        /// Gets the mocked <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <value>The built <see cref="ICacheEntry"/>.</value>
        protected MockedCacheEntry MemoryCacheEntry { get; private set; }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithKey(object key)
        {
            this.MemoryCacheEntry.Key = key;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithValue(object value)
        {
            this.MemoryCacheEntry.Value = value;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.MemoryCacheEntry.AbsoluteExpiration = absoluteExpiration;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.MemoryCacheEntry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithPriority(CacheItemPriority priority)
        {
            this.MemoryCacheEntry.Priority = priority;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndMemoryCacheEntryBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.MemoryCacheEntry.SlidingExpiration = slidingExpiration;
            return this;
        }

        /// <inheritdoc />
        public IMemoryCacheEntryBuilder AndAlso() => this;

        internal ICacheEntry GetMockedMemoryCacheEntry()
        {
            if (this.MemoryCacheEntry.Key == null)
            {
                throw new InvalidOperationException("Cache entry key must be provided. 'WithKey' method must be called with а non-null value.");
            }
            
            return this.MemoryCacheEntry;
        }
    }
}
