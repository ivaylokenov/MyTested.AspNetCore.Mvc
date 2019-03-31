namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/> tests.
    /// </summary>
    public interface IAndResponseCacheAttributeTestBuilder : IResponseCacheAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IResponseCacheAttributeTestBuilder"/>.</returns>
        IResponseCacheAttributeTestBuilder AndAlso();
    }
}
