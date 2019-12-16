namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    /// <summary>
    /// Used for setting <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> key.
    /// </summary>
    public interface IDistributedCacheEntryKeyTestBuilder
    {
        /// <summary>
        /// Sets the key of the built <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entry.
        /// </summary>
        /// <param name="key">Cache entry key to set.</param>
        /// <returns>The same <see cref="IAndDistributedCacheEntryTestBuilder"/>.</returns>
        IAndDistributedCacheEntryTestBuilder WithKey(string key);
    }
}
