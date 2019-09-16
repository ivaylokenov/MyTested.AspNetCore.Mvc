namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using Contracts.Data.DistributedCache;

    public class DistributedCacheEntryBuilder : IDistributedCacheEntryKeyBuilder, IAndDistributedCacheEntryBuilder
    {
        public IAndDistributedCacheEntryBuilder WithKey(string key)
        {
            throw new NotImplementedException();
        }

        public IAndDistributedCacheEntryBuilder WithValue(byte[] value)
        {
            throw new NotImplementedException();
        }

        public IAndDistributedCacheEntryBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public IAndDistributedCacheEntryBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public IAndDistributedCacheEntryBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public IDistributedCacheEntryBuilder AndAlso()
        {
            throw new NotImplementedException();
        }
    }
}
