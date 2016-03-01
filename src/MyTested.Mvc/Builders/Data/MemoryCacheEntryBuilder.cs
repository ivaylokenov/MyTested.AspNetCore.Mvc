namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;
    using System;

    public class MemoryCacheEntryBuilder : IAndMemoryCacheEntryTestBuilder
    {
        public MemoryCacheEntryBuilder()
        {
            this.CacheEntry = new MockedCacheEntry();
        }
        
        protected MockedCacheEntry CacheEntry { get; private set; }

        public virtual IAndMemoryCacheEntryTestBuilder WithKey(object key)
        {
            this.CacheEntry.Key = key;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithValue(object value)
        {
            this.CacheEntry.Value = value;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.CacheEntry.Options.AbsoluteExpiration = absoluteExpiration;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.CacheEntry.Options.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithPriority(CacheItemPriority priority)
        {
            this.CacheEntry.Options.Priority = priority;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.CacheEntry.Options.SlidingExpiration = slidingExpiration;
            return this;
        }

        public IMemoryCacheEntryTestBuilder AndAlso()
        {
            return this;
        }
    }
}
