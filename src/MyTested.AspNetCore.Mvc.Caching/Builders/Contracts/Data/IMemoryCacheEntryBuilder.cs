namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Used for building <see cref="IMemoryCache"/> entry.
    /// </summary>
    public interface IMemoryCacheEntryBuilder
    {
        /// <summary>
        /// Sets the value of the built <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <param name="value">Cache entry value to set.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithValue(object value);

        /// <summary>
        /// Sets the <see cref="MemoryCacheEntryOptions.AbsoluteExpiration"/> value to the built <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <param name="absoluteExpiration">Absolute expiration value to set.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration);

        /// <summary>
        /// Sets the <see cref="MemoryCacheEntryOptions.AbsoluteExpirationRelativeToNow"/> value to the built <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <param name="absoluteExpirationRelativeToNow">Absolute expiration relative to now value to set.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow);

        /// <summary>
        /// Sets the <see cref="MemoryCacheEntryOptions.Priority"/> value to the built <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <param name="priority"><see cref="CacheItemPriority"/> value to set.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithPriority(CacheItemPriority priority);

        /// <summary>
        /// Sets the <see cref="MemoryCacheEntryOptions.SlidingExpiration"/> value to the built <see cref="IMemoryCache"/> entry.
        /// </summary>
        /// <param name="slidingExpiration">Sliding expiration value to set.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithSlidingExpiration(TimeSpan? slidingExpiration);
    }
}
