namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Used for building mocked <see cref="IDistributedCache"/>.
    /// </summary>
    public interface IWithoutDistributedCacheBuilder
    {
        /// <summary>
        /// Remove cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCacheBuilder"/>.</returns>
        IAndWithoutDistributedCacheBuilder WithoutEntry(string key);

        /// <summary>
        /// Remove cache entries to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCacheBuilder"/>.</returns>
        IAndWithoutDistributedCacheBuilder WithoutEntries(IEnumerable<string> keys);

        /// <summary>
        /// Remove cache params to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCacheBuilder"/>.</returns>
        IAndWithoutDistributedCacheBuilder WithoutEntries(params string[] keys);

        /// <summary>
        /// Clear all entries persisted into the <see cref="IDistributedCache"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutDistributedCacheBuilder"/>.</returns>
        IAndWithoutDistributedCacheBuilder WithoutAllEntries();
    }
}
