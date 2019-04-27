namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TAuthenticationResultTestBuilder">Type of authentication result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> 
        : IBaseTestBuilderWithActionResult
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
