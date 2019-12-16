namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;

    public class CustomDistributedCache : IDistributedCache
    {
        public byte[] Get(string key) => new byte[0];

        public Task<byte[]> GetAsync(string key, CancellationToken token = default) 
            => Task.FromResult(this.Get(key));

        public void Refresh(string key) => this.Get(key);

        public Task RefreshAsync(string key, CancellationToken token = default) 
            => Task.CompletedTask;

        public void Remove(string key) {}

        public Task RemoveAsync(string key, CancellationToken token = default) 
            => Task.CompletedTask;

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options) {}

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
            => Task.CompletedTask;
    }
}
