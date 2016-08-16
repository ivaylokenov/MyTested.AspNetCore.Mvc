namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry details tests.
    /// </summary>
    public interface IAndMemoryCacheEntryDetailsTestBuilder<TEntry> : IMemoryCacheEntryDetailsTestBuilder<TEntry>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry details tests.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheEntryDetailsTestBuilder{TEntry}"/>.</returns>
        IMemoryCacheEntryDetailsTestBuilder<TEntry> AndAlso();
    }
}
