namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedCacheMock : IDistributedCache
    {
        int Count { get; }

        bool TryGetCacheEntryOptions(string key, out DistributedCacheEntryOptions cacheEntryOptions);

        Dictionary<string, byte[]> GetCacheAsDictionary();
    }
}
