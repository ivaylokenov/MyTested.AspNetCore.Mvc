namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheEntryMock
    {
        public DistributedCacheEntryMock()
        {
            this.Options = new DistributedCacheEntryOptions();
        }

        public DistributedCacheEntryMock(byte[] value, DistributedCacheEntryOptions options)
        {
            this.Value = value;
            this.Options = options;
        }

        public byte[] Value { get; set; }

        public DistributedCacheEntryOptions Options { get; set; }
    }
}
