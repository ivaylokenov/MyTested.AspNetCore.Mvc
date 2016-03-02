namespace MyTested.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMockedMemoryCache : IMemoryCache
    {
        int Count { get; }

        bool TryGetCacheEntry(object key, out IMockedMemoryCacheEntry value);

        IDictionary<object, object> GetCacheAsDictionary();
    }
}
