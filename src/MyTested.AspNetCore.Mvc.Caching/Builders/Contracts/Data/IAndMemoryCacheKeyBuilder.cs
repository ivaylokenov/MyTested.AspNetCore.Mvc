namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for setting <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> key.
    /// </summary>
    public interface IMemoryCacheEntryKeyBuilder
    {
        /// <summary>
        /// Sets the key of the built <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry.
        /// </summary>
        /// <param name="key">Cache entry key to set.</param>
        /// <returns>The same <see cref="IMemoryCacheEntryBuilder"/>.</returns>
        IAndMemoryCacheEntryBuilder WithKey(object key);
    }
}
