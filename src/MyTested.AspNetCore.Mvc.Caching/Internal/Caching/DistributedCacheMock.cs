namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheMock : IDistributedCacheMock
    {
        private readonly IDictionary<string, DistributedCacheEntry> cache;

        public DistributedCacheMock()
            => this.cache = new ConcurrentDictionary<string, DistributedCacheEntry>();

        public byte[] Get(string key)
        {
            this.cache.TryGetValue(key, out var entryValue);
            return entryValue?.Value ?? null;
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            var entry = this.Get(key);
            return Task.FromResult(entry);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            var entry = new DistributedCacheEntry(value, options);
            this.cache.Add(key, entry);
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            this.Set(key, value, options);
            return Task.CompletedTask;
        }

        public void Refresh(string key) => this.Get(key);

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

        public int Count => this.cache.Count;

        public bool TryGetCacheEntryOptions(string key, out DistributedCacheEntryOptions cacheEntryOptions)
        {
            if(this.cache.TryGetValue(key, out var entry))
            {
                cacheEntryOptions = entry.Options;
                return true;
            }

            cacheEntryOptions = null;
            return false;
        }

        public Dictionary<string, byte[]> GetCacheAsDictionary()
            => this.cache.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value);

        public void Dispose()
        {
            this.cache.Clear();
        }
    }
}
