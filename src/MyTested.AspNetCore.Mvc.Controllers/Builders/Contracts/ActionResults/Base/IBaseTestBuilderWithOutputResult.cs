namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TOutputResultTestBuilder">Type of output result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder>
        : IBaseTestBuilderWithStatusCodeResult<TOutputResultTestBuilder>
        where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
