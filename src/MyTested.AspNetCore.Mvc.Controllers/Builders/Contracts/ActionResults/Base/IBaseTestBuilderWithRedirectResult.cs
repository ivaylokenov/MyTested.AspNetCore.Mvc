namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TRedirectResultTestBuilder">Type of redirect result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> : 
        IBaseTestBuilderWithUrlHelperResult<TRedirectResultTestBuilder>
        where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
