namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data.MemoryCache
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> tests.
    /// </summary>
    public interface IAndMemoryCacheTestBuilder : IMemoryCacheTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheTestBuilder"/>.</returns>
        IMemoryCacheTestBuilder AndAlso();
    }
}
