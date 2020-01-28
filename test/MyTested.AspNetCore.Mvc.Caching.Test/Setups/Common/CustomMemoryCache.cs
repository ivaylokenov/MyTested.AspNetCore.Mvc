namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;

    public class CustomMemoryCache : IMemoryCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            return new MemoryCacheEntryMock(key);
        }
        
        public void Dispose()
        {
        }

        public void Remove(object key)
        {
        }

        public object Set(object key, object value, MemoryCacheEntryOptions options)
        {
            return new object();
        }

        public bool TryGetValue(object key, out object value)
        {
            value = null;
            return false;
        }
    }
}
