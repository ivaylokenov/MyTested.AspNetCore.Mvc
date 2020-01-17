namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Contracts;

    public class MemoryCacheMock : IMemoryCacheMock
    {
        private static readonly TestLocal<IDictionary<object, ICacheEntry>> Current
            = new TestLocal<IDictionary<object, ICacheEntry>>();

        private readonly IDictionary<object, ICacheEntry> cache;

        public MemoryCacheMock()
            => this.cache = this.GetCurrentCache();

        public int Count => this.cache.Count;

        public void Remove(object key)
        {
            if (this.cache.ContainsKey(key))
            {
                this.cache.Remove(key);
            }
        }

        public ICacheEntry CreateEntry(object key)
        {
            var value = new MemoryCacheEntryMock(key);
            this.cache[key] = value;
            return value;
        }

        public bool TryGetValue(object key, out object value)
        {
            if (this.TryGetCacheEntry(key, out var cacheEntry))
            {
                value = cacheEntry.Value;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGetCacheEntry(object key, out ICacheEntry value)
        {
            if (this.cache.ContainsKey(key))
            {
                value = this.cache[key];
                return true;
            }

            value = null;
            return false;
        }

        public IDictionary<object, object> GetCacheAsDictionary()
            => this.cache.ToDictionary(c => c.Key, c => c.Value.Value);

        public void RemoveKeys(IEnumerable<object> keys)
        {
            foreach (var key in keys)
            {
                this.Remove(key);
            }
        }

        public void ClearCache()
            => this.cache.Clear();

        public void Dispose()
            => this.ClearCache();

        private IDictionary<object, ICacheEntry> GetCurrentCache()
        {
            var result = Current.Value;
            if (result != null)
            {
                return result;
            }

            var newCache = new Dictionary<object, ICacheEntry>();
            Current.Value = newCache;
            return newCache;
        }
    }
}
