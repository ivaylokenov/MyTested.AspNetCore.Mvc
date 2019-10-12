namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Used for building mocked <see cref="IDistributedCache"/>.
    /// </summary>
    public interface IDistributedCacheBuilder
    {
        /// <summary>
        /// Adds cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntry(string key, byte[] value);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">String value of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntry(string key, string value);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <param name="options"><see cref="DistributedCacheEntryOptions"/> of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntry(string key, byte[] value, DistributedCacheEntryOptions options);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">String value of the cache entry.</param>
        /// <param name="options"><see cref="DistributedCacheEntryOptions"/> of the cache entry.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntry(string key, string value, DistributedCacheEntryOptions options);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="distributedCacheEntryBuilder">Builder for creating cache entry.</param>
        /// <returns>The same <see cref="IDistributedCacheEntryKeyBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntry(Action<IDistributedCacheEntryKeyBuilder> distributedCacheEntryBuilder);

        /// <summary>
        /// Adds cache entries to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="entries">Dictionary of cache entries.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntries(IDictionary<string, byte[]> entries);

        /// <summary>
        /// Adds cache entries to the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <param name="entries">Dictionary of cache entries.</param>
        /// <returns>The same <see cref="IAndDistributedCacheBuilder"/>.</returns>
        IAndDistributedCacheBuilder WithEntries(IDictionary<string, string> entries);
    }
}
