namespace MyTested.Mvc.Internal.Caching
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

#if NET451
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting;
#elif DOTNET5_6
    using System.Threading;
#endif

    public class MockedMemoryCache : IMemoryCache
    {
#if NET451
        private const string LogicalDataKey = "__MemoryCache_Current__";
#elif DOTNET5_6
        private static readonly AsyncLocal<IDictionary<object, MockedCacheEntry>> МemoryCacheCurrent = new AsyncLocal<IDictionary<object, MockedCacheEntry>>();
#endif
        private readonly IDictionary<object, MockedCacheEntry> cache;

        public MockedMemoryCache()
        {
            this.cache = this.GetCurrentCache();
        }

        public IEntryLink CreateLinkingScope()
        {
            return new MockedEntryLink();
        }

        public void Dispose()
        {
            this.cache.Clear();
        }

        public void Remove(object key)
        {
            if (this.cache.ContainsKey(key))
            {
                this.cache.Remove(key);
            }
        }

        public object Set(object key, object value, MemoryCacheEntryOptions options)
        {
            this.cache[key] = new MockedCacheEntry(key, value, options);
            return value;
        }

        public bool TryGetValue(object key, out object value)
        {
            if (this.cache.ContainsKey(key))
            {
                value = this.cache[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        private IDictionary<object, MockedCacheEntry> GetCurrentCache()
        {
#if NET451
            var handle = CallContext.LogicalGetData(LogicalDataKey) as ObjectHandle;
            var result = handle?.Unwrap() as IDictionary<object, MockedCacheEntry>;
            if (result == null)
            {
                result = new Dictionary<object, MockedCacheEntry>();
                CallContext.LogicalSetData(LogicalDataKey, new ObjectHandle(result));
            }

            return result;
#elif DOTNET5_6
            var result = МemoryCacheCurrent.Value;
            if (result == null)
            {
                result = new Dictionary<object, MockedCacheEntry>();
                МemoryCacheCurrent.Value = result;
            }

            return result;
#endif
        }
    }
}
