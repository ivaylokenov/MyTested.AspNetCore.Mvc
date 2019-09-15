namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using Microsoft.Extensions.Caching.Distributed;

    internal class DistributedCacheEntry
    {
        internal DistributedCacheEntry()
        {
        }

        internal DistributedCacheEntry(byte[] value, DistributedCacheEntryOptions options)
        {
            this.Value = value;
            this.Options = options;
        }

        internal byte[] Value { get; set; }

        internal DistributedCacheEntryOptions Options { get; set; }
    }
}
