namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheMock : IMemoryCache
    {
        int Count { get; }

        bool TryGetCacheEntry(object key, out ICacheEntry value);

        IDictionary<object, object> GetCacheAsDictionary();

        void RemoveKeys(IEnumerable<object> keys);

        void ClearCache();
    }
}
