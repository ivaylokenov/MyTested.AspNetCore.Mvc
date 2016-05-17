namespace MyTested.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;
    using Utilities.Validators;

    public class MockedCacheEntry : ICacheEntry
    {
        private readonly IList<IChangeToken> expirationTokens;
        private readonly IList<PostEvictionCallbackRegistration> postEvictionCallbacks;

        private object key;

        public MockedCacheEntry()
        {
            this.expirationTokens = new List<IChangeToken>();
            this.postEvictionCallbacks = new List<PostEvictionCallbackRegistration>();
        }

        public MockedCacheEntry(object key)
            : this()
        {
            this.Key = key;
        }

        public object Key
        {
            get
            {
                return this.key;
            }

            internal set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.key));
                this.key = value;
            }
        }

        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public IList<IChangeToken> ExpirationTokens => this.expirationTokens;

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks => this.postEvictionCallbacks;

        public CacheItemPriority Priority { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public object Value { get; set; }

        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
