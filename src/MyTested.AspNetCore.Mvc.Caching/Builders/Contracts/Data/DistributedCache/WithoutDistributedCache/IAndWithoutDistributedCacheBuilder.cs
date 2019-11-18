namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> builder.
    /// </summary>
    public interface IAndWithoutDistributedCacheBuilder : IWithoutDistributedCacheBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>.
        /// </summary>
        /// <returns></returns>
        IWithoutDistributedCacheBuilder AndAlso();
    }
}
