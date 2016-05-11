namespace MyTested.Mvc.Internal.Caching
{
    using System.Linq;
    using System.Collections.Generic;
#if NET451
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting;
#elif NETSTANDARD1_5
    using System.Threading;
#endif
    using Contracts;
    using Microsoft.Extensions.Caching.Memory;

    public class MockedMemoryCache : IMockedMemoryCache
    {
        private static readonly IEntryLink DefaultEntryLink = new MockedEntryLink();

#if NET451
        private const string DataKey = "__MemoryCache_Current__";
#elif NETSTANDARD1_5
        private static readonly AsyncLocal<IDictionary<object, IMockedMemoryCacheEntry>> МemoryCacheCurrent = new AsyncLocal<IDictionary<object, IMockedMemoryCacheEntry>>();
#endif
        private readonly IDictionary<object, IMockedMemoryCacheEntry> cache;

        public MockedMemoryCache()
        {
            this.cache = this.GetCurrentCache();
        }

        public int Count => this.cache.Count;

        public IEntryLink CreateLinkingScope() => DefaultEntryLink;

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

        public ICacheEntry CreateEntry(object key)
        {
            this.cache[key] = new MockedMemoryCacheEntry(key, value, options);
            return value;
        }

        public bool TryGetValue(object key, out object value)
        {
            IMockedMemoryCacheEntry cacheEntry;
            if (this.TryGetCacheEntry(key, out cacheEntry))
            {
                value = cacheEntry.Value;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGetCacheEntry(object key, out IMockedMemoryCacheEntry value)
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

        public IDictionary<object, object> GetCacheAsDictionary()
        {
            return this.cache.ToDictionary(c => c.Key, c => c.Value.Value);
        }

        private IDictionary<object, IMockedMemoryCacheEntry> GetCurrentCache()
        {
#if NET451
            var handle = CallContext.GetData(DataKey) as ObjectHandle;
            var result = handle?.Unwrap() as IDictionary<object, IMockedMemoryCacheEntry>;
            if (result == null)
            {
                result = new Dictionary<object, IMockedMemoryCacheEntry>();
                CallContext.SetData(DataKey, new ObjectHandle(result));
            }

            return result;
#elif NETSTANDARD1_5
            var result = МemoryCacheCurrent.Value;
            if (result == null)
            {
                result = new Dictionary<object, IMockedMemoryCacheEntry>();
                МemoryCacheCurrent.Value = result;
            }

            return result;
#endif
        }
    }
}
