namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entry tests.
    /// </summary>
    public interface IAndDistributedCacheEntryTestBuilder : IDistributedCacheEntryTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <returns>The same <see cref="IDistributedCacheEntryTestBuilder"/>.</returns>
        IDistributedCacheEntryTestBuilder AndAlso();
    }
}
