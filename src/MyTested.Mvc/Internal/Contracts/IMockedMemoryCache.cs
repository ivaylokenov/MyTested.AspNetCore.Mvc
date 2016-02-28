namespace MyTested.Mvc.Internal.Contracts
{
    using Microsoft.Extensions.Caching.Memory;

    public interface IMockedMemoryCache : IMemoryCache
    {
        bool TryGetCacheEntry(object key, out IMockedCacheEntry value);
    }
}
