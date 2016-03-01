namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.Caching;
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;
    using System;

    public class MemoryCacheEntryBuilder : IAndMemoryCacheEntryTestBuilder
    {
        public MemoryCacheEntryBuilder()
        {
            this.MemoryCacheEntry = new MockedMemoryCacheEntry();
        }
        
        protected MockedMemoryCacheEntry MemoryCacheEntry { get; private set; }

        public virtual IAndMemoryCacheEntryTestBuilder WithKey(object key)
        {
            this.MemoryCacheEntry.Key = key;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithValue(object value)
        {
            this.MemoryCacheEntry.Value = value;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.MemoryCacheEntry.Options.AbsoluteExpiration = absoluteExpiration;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.MemoryCacheEntry.Options.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithPriority(CacheItemPriority priority)
        {
            this.MemoryCacheEntry.Options.Priority = priority;
            return this;
        }

        public virtual IAndMemoryCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.MemoryCacheEntry.Options.SlidingExpiration = slidingExpiration;
            return this;
        }

        public IMemoryCacheEntryTestBuilder AndAlso()
        {
            return this;
        }

        internal IMockedMemoryCacheEntry GetMockedMemoryCacheEntry()
        {
            if (this.MemoryCacheEntry.Key == null)
            {
                throw new InvalidOperationException("Cache entry key must be provided. 'WithKey' method must be called on the memory cache entry builder in order to run this test case successfully.");
            }


            return this.MemoryCacheEntry;
        }
    }
}
