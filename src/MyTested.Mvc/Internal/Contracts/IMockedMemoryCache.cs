namespace MyTested.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMockedMemoryCache : IMemoryCache
    {
        bool TryGetCacheEntry(object key, out IMockedCacheEntry value);

        IDictionary<object, object> GetCacheAsDictionary();
    }
}
