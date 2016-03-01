namespace MyTested.Mvc.Internal.Caching
{
    using Microsoft.Extensions.Caching.Memory;
    using Internal.Contracts;
    using Utilities.Validators;

    public class MockedCacheEntry : IMockedCacheEntry
    {
        private object key;

        public MockedCacheEntry()
        {
            this.Options = new MemoryCacheEntryOptions();
        }

        public MockedCacheEntry(
            object key,
            object value,
            MemoryCacheEntryOptions options)
        {;
            this.Key = key;
            this.Value = value;
            this.Options = options ?? new MemoryCacheEntryOptions();
        }

        public object Key
        {
            get
            {
                return this.key;
            }

            internal set
            {
                CommonValidator.CheckForNullReference(key, nameof(key));
                this.key = value;
            }
        }

        public object Value { get; internal set; }

        public MemoryCacheEntryOptions Options { get; internal set; }
    }
}
