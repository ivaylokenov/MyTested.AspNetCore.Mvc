namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    using MemoryCache;

    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entry builder.
    /// </summary>
    public interface IAndDistributedCacheEntryBuilder : IDistributedCacheEntryBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheEntryBuilder"/>.</returns>
        IDistributedCacheEntryBuilder AndAlso();
    }
}
