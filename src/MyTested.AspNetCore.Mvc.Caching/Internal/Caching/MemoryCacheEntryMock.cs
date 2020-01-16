namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;

    public class MemoryCacheEntryMock : ICacheEntry
    {
        public MemoryCacheEntryMock()
        {
            this.ExpirationTokens = new List<IChangeToken>();
            this.PostEvictionCallbacks = new List<PostEvictionCallbackRegistration>();
        }

        public MemoryCacheEntryMock(object key)
            : this()
        {
            this.Key = key;
        }

        public object Key { get; internal set; }

        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public IList<IChangeToken> ExpirationTokens { get; }

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }

        public CacheItemPriority Priority { get; set; }

        public long? Size { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public object Value { get; set; }

        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
