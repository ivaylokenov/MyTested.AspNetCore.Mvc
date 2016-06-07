namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry tests.
    /// </summary>
    public interface IAndMemoryCacheEntryTestBuilder : IMemoryCacheEntryTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entries.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheEntryTestBuilder"/>.</returns>
        IMemoryCacheEntryTestBuilder AndAlso();
    }
}
