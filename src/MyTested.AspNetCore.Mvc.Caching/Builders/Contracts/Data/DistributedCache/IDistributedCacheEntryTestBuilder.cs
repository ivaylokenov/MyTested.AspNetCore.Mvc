namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.DistributedCache
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Used for testing <see cref="IDistributedCache"/> entry.
    /// </summary>
    public interface IDistributedCacheEntryTestBuilder
    {
        /// <summary>
        /// Sets the value of the built <see cref="IDistributedCache"/> entry.
        /// </summary>
        /// <param name="value">Cache entry value to set.</param>
        /// <returns>The same <see cref="IAndDistributedCacheEntryTestBuilder"/>.</returns>
        IAndDistributedCacheEntryTestBuilder WithValue(byte[] value);

        /// <summary>
        /// Sets the <see cref="DistributedCacheEntryOptions.AbsoluteExpiration"/> value to the built <see cref="IDistributedCache"/> entry.
        /// </summary>
        /// <param name="absoluteExpiration">Absolute expiration value to set.</param>
        /// <returns>The same <see cref="IAndDistributedCacheEntryTestBuilder"/>.</returns>
        IAndDistributedCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration);

        /// <summary>
        /// Sets the <see cref="DistributedCacheEntryOptions.AbsoluteExpirationRelativeToNow"/> value to the built <see cref="IDistributedCache"/> entry.
        /// </summary>
        /// <param name="absoluteExpirationRelativeToNow">Absolute expiration relative to now value to set.</param>
        /// <returns>The same <see cref="IAndDistributedCacheEntryTestBuilder"/>.</returns>
        IAndDistributedCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow);

        /// <summary>
        /// Sets the <see cref="DistributedCacheEntryOptions.SlidingExpiration"/> value to the built <see cref="IDistributedCache"/> entry.
        /// </summary>
        /// <param name="slidingExpiration">Sliding expiration value to set.</param>
        /// <returns>The same <see cref="IAndDistributedCacheEntryTestBuilder"/>.</returns>
        IAndDistributedCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration);
    }
}
