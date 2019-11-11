namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Used for building mocked <see cref="IDistributedCache"/>.
    /// </summary>
    public interface IWithoutDistributedCache
    {
        /// <summary>
        /// Remove cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCache"/>.</returns>
        IAndWithoutDistributedCache WithoutEntry(object key);

        /// <summary>
        /// Remove cache entries to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCache"/>.</returns>
        IAndWithoutDistributedCache WithoutEntries(IEnumerable<object> keys);

        /// <summary>
        /// Remove cache params to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="keys">Keys of the cache entries.</param>
        /// <returns>The same <see cref="IAndWithoutDistributedCache"/>.</returns>
        IAndWithoutDistributedCache WithoutEntries(params object[] keys);

        /// <summary>
        /// Clear all entries persisted into the <see cref="IDistributedCache"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutDistributedCache"/>.</returns>
        IAndWithoutDistributedCache WithoutAllEntries();
    }
}
