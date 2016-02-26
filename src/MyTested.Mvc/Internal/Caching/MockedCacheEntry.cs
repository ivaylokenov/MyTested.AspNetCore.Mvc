using Microsoft.Extensions.Caching.Memory;
using MyTested.Mvc.Utilities.Validators;

namespace MyTested.Mvc.Internal.Caching
{
    public class MockedCacheEntry
    {
        public MockedCacheEntry(
            object key,
            object value,
            MemoryCacheEntryOptions options)
        {
            CommonValidator.CheckForNullReference(key, nameof(key));
            this.Key = key;
            this.Value = value;
            this.Options = options ?? new MemoryCacheEntryOptions();
        }

        public object Key { get; private set; }

        public object Value { get; private set; }

        public MemoryCacheEntryOptions Options { get; private set; }
    }
}
