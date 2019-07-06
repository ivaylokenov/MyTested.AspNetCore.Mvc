namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TRouteValuesResultTestBuilder">Type of route values result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder>
        : IBaseTestBuilderWithUrlHelperResult<TRouteValuesResultTestBuilder>
        where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
