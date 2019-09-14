namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheEntry
    {
        public DistributedCacheEntry()
        {
        }

        public DistributedCacheEntry(byte[] value, DistributedCacheEntryOptions options)
        {
            this.Value = value;
            this.Options = options;
        }

        public byte[] Value { get; set; }

        public DistributedCacheEntryOptions Options { get; set; }
    }
}
