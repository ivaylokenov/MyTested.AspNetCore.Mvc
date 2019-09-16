namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Used for testing <see cref="IMemoryCache"/>.
    /// </summary>
    public interface IMemoryCacheTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with the provided key.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntryWithKey(object key);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with the provided value.
        /// </summary>
        /// <typeparam name="TValue">Type of the cache entry value.</typeparam>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntryWithValue<TValue>(TValue value);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with value of the provided type.
        /// </summary>
        /// <typeparam name="TValue">Type of the cache entry value.</typeparam>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntryOfType<TValue>();

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntry(object key, object value);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with value of the provided type and the given key.
        /// </summary>
        /// <typeparam name="TValue">Type of the cache entry value.</typeparam>
        /// <param name="key">Key of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntryOfType<TValue>(object key);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains entry with the provided key, corresponding value and the deeply equal options to the given ones.
        /// </summary>
        /// <param name="key">Key of the cache entry.</param>
        /// <param name="value">Value of the cache entry.</param>
        /// <param name="options"><see cref="MemoryCacheEntryOptions"/> of the cache entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntry(object key, object value, MemoryCacheEntryOptions options);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains specific entry by using a builder. 
        /// </summary>
        /// <param name="memoryCacheEntryTestBuilder">Builder for setting specific cache entry tests.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntry(Action<IMemoryCacheEntryKeyTestBuilder> memoryCacheEntryTestBuilder);

        /// <summary>
        /// Tests whether the <see cref="IMemoryCache"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Dictionary of cache entries.</param>
        /// <returns>The same <see cref="IAndMemoryCacheBuilder"/>.</returns>
        IAndMemoryCacheTestBuilder ContainingEntries(IDictionary<object, object> entries);
    }
}
