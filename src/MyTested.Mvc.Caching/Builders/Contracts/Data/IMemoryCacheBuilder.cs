namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/>.
    /// </summary>
    public interface IMemoryCacheBuilder
    {
        /// <summary>
        /// Adds cache entry to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheBuilder WithEntry(object key, object value);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <param name="options"><see cref="MemoryCacheEntryOptions"/> of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheBuilder WithEntry(object key, object value, MemoryCacheEntryOptions options);

        /// <summary>
        /// Adds cache entry to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="memoryCacheEntryBuilder">Builder for creating cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheBuilder WithEntry(Action<IMemoryCacheEntryTestBuilder> memoryCacheEntryBuilder);

        /// <summary>
        /// Adds cache entries to the mocked <see cref="IMemoryCache"/>.
        /// </summary>
        /// <param name="entries">Dictionary of cache entries.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheBuilder WithEntries(IDictionary<object, object> entries);
    }
}
