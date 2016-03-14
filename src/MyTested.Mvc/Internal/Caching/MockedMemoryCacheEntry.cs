namespace MyTested.Mvc.Internal.Caching
{
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;
    using Utilities.Validators;

    public class MockedMemoryCacheEntry : IMockedMemoryCacheEntry
    {
        private object key;

        public MockedMemoryCacheEntry()
        {
            this.Options = new MemoryCacheEntryOptions();
        }

        public MockedMemoryCacheEntry(
            object key,
            object value,
            MemoryCacheEntryOptions options)
        {
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
                CommonValidator.CheckForNullReference(value, nameof(key));
                this.key = value;
            }
        }

        public object Value { get; internal set; }

        public MemoryCacheEntryOptions Options { get; internal set; }
    }
}
