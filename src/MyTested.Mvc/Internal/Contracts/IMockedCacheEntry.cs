namespace MyTested.Mvc.Internal.Contracts
{
    using Microsoft.Extensions.Caching.Memory;

    public interface IMockedCacheEntry
    {
        object Key { get; }

        object Value { get; }

        MemoryCacheEntryOptions Options { get; }
    }
}
