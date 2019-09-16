namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="IDistributedCache"/>
    /// </summary>
    public interface IDistributedCacheTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains entry with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value);

        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains entry with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <param name="options"><see cref="DistributedCacheEntryOptions"/> of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value, DistributedCacheEntryOptions options);

        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains specific entry by using a builder. 
        /// </summary>
        /// <param name="distributedCacheEntryTestBuilder">Builder for setting specific cache entry tests.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntry(Action<IDistributedCacheEntryKeyTestBuilder> distributedCacheEntryTestBuilder);

        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains entry with the provided key.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntryWithKey(string key);

        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains entry with the provided value.
        /// </summary>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntryWithValue(byte[] value);

        /// <summary>
        /// Tests whether the <see cref="IDistributedCache"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Dictionary of cache entries.</param>
        /// <returns>The same <see cref="IAndDistributedCacheTestBuilder"/>.</returns>
        IAndDistributedCacheTestBuilder ContainingEntries(IDictionary<string, byte[]> entries);

    }
}
