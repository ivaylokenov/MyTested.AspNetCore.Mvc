namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheEntryTestBuilder
    {
        IAndMemoryCacheEntryTestBuilder WithKey(object key);

        IAndMemoryCacheEntryTestBuilder WithValue(object value);

        IAndMemoryCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration);

        IAndMemoryCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow);

        IAndMemoryCacheEntryTestBuilder WithPriority(CacheItemPriority priority);

        IAndMemoryCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration);
    }
}
