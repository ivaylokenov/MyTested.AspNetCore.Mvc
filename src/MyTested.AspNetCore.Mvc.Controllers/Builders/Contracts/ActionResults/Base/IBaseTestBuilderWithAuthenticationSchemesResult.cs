namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders for <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> with authentication schemes.
    /// </summary>
    /// <typeparam name="TAuthenticationResultTestBuilder">
    /// Type of authentication result test builder to use as a return type for common methods.
    /// </typeparam>
    public interface IBaseTestBuilderWithAuthenticationSchemesResult<TAuthenticationResultTestBuilder>
        : IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder>
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
