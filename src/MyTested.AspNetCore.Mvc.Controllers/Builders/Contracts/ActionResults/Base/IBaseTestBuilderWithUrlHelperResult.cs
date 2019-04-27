namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base interface for all test builders with URL helper <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="TUrlHelperResultTestBuilder">Type of URL helper result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithUrlHelperResult<TUrlHelperResultTestBuilder> 
        : IBaseTestBuilderWithActionResult
        where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
