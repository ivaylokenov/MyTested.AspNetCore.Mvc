namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheMock : IDistributedCacheMock
    {
        private readonly IDictionary<string, DistributedCacheEntry> cache;

        public DistributedCacheMock()
            => this.cache = new ConcurrentDictionary<string, DistributedCacheEntry>();

        public int Count => this.cache.Count;

        public byte[] Get(string key)
        {
            return this.cache[key].Value;
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            return Task.FromResult(this.Get(key));
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            var entry = new DistributedCacheEntry(value, options);
            this.cache[key] = entry;
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            this.Set(key, value, options);
            return Task.CompletedTask;
        }

        public void Refresh(string key)
        {
            this.Get(key);
        }

        public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
        {
            this.Refresh(key);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            if (this.cache.ContainsKey(key))
            {
                this.cache.Remove(key);
            }
        }

        public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
        {
            this.cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
