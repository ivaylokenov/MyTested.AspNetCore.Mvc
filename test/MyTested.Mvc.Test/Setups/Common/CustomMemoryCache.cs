namespace MyTested.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class CustomMemoryCache : IMemoryCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            return null;
        }
        
        public void Dispose()
        {
        }

        public void Remove(object key)
        {
        }

        public object Set(object key, object value, MemoryCacheEntryOptions options)
        {
            return null;
        }

        public bool TryGetValue(object key, out object value)
        {
            value = null;
            return false;
        }
    }
}
