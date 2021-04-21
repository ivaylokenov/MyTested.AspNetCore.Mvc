namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedCacheMock : IDistributedCache, IDisposable
    {
        int Count { get; }

        bool TryGetCacheEntryOptions(string key, out DistributedCacheEntryOptions cacheEntryOptions);

        Dictionary<string, byte[]> GetCacheAsDictionary();

        void RemoveKeys(IEnumerable<string> keys);

        void ClearCache();
    }
}
