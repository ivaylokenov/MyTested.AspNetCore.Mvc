namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using Contracts.Data.DistributedCache;

    public class DistributedCacheEntryTestBuilder : DistributedCacheEntryBuilder, IDistributedCacheEntryKeyTestBuilder, IAndDistributedCacheEntryTestBuilder
    {
        public new IAndDistributedCacheEntryTestBuilder WithKey(string key)
        {
            throw new NotImplementedException();
        }

        public new IAndDistributedCacheEntryTestBuilder WithValue(byte[] value)
        {
            throw new NotImplementedException();
        }

        public new IAndDistributedCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public new IAndDistributedCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public new IAndDistributedCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public new IDistributedCacheEntryTestBuilder AndAlso()
        {
            throw new NotImplementedException();
        }
    }
}
