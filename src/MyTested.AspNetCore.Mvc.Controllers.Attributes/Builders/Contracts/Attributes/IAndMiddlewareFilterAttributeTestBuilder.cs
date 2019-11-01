namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.MiddlewareFilterAttribute"/> tests.
    /// </summary>
    public interface IAndMiddlewareFilterAttributeTestBuilder : IMiddlewareFilterAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.MiddlewareFilterAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IMiddlewareFilterAttributeTestBuilder"/>.</returns>
        IMiddlewareFilterAttributeTestBuilder AndAlso();
    }
}
