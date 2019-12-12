namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> tests.
    /// </summary>
    public interface IAndDistributedCacheTestBuilder : IDistributedCacheTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>
        /// </summary>
        /// <returns></returns>
        IDistributedCacheTestBuilder AndAlso();
    }
}
