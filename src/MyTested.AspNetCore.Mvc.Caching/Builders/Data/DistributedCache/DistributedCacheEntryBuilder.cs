namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using Contracts.Data.DistributedCache;
    using Internal.Caching;
    using Utilities;

    public class DistributedCacheEntryBuilder : IDistributedCacheEntryKeyBuilder, IAndDistributedCacheEntryBuilder
    {
        internal DistributedCacheEntry DistributedCacheEntry { get; set; }
        internal string EntryKey { get; set; }

        public DistributedCacheEntryBuilder()
            => this.DistributedCacheEntry = new DistributedCacheEntry();
        

        public IAndDistributedCacheEntryBuilder WithKey(string key)
        {
            this.EntryKey = key;
            return this;
        }

        public IAndDistributedCacheEntryBuilder WithValue(byte[] value)
        {
            this.DistributedCacheEntry.Value = value;
            return this;
        }

        public IAndDistributedCacheEntryBuilder WithValue(string value)
        {
            this.WithValue(BytesHelper.GetBytes(value));
            return this;
        }

        public IAndDistributedCacheEntryBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.DistributedCacheEntry.Options.AbsoluteExpiration = absoluteExpiration;
            return this;
        }

        public IAndDistributedCacheEntryBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.DistributedCacheEntry.Options.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            return this;
        }

        public IAndDistributedCacheEntryBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.DistributedCacheEntry.Options.SlidingExpiration = slidingExpiration;
            return this;
        }

        public IDistributedCacheEntryBuilder AndAlso() => this;
    }
}
