namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry details.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry value.</typeparam>
    public interface IMemoryCacheEntryDetailsTestBuilder<TValue> : IMemoryCacheEntryTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryTestBuilder Passing(Action<TValue> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry.</param>
        /// <returns>The same <see cref="IAndMemoryCacheEntryTestBuilder"/>.</returns>
        IAndMemoryCacheEntryTestBuilder Passing(Func<TValue, bool> predicate);
    }
}
