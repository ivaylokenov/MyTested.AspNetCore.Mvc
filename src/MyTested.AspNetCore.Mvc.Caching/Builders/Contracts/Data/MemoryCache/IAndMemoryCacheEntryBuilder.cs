namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.MemoryCache
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry builder.
    /// </summary>
    public interface IAndMemoryCacheEntryBuilder : IMemoryCacheEntryBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entries.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheEntryBuilder"/>.</returns>
        IMemoryCacheEntryBuilder AndAlso();
    }
}
