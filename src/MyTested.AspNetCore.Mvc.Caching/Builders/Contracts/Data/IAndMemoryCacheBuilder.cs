namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> builder.
    /// </summary>
    public interface IAndMemoryCacheBuilder : IMemoryCacheBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
        /// </summary>
        /// <returns>The same <see cref="IMemoryCacheBuilder"/>.</returns>
        IMemoryCacheBuilder AndAlso();
    }
}
