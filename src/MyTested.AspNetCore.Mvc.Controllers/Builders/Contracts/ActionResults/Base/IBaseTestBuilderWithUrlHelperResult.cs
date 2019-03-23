namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base interface for all test builders with URL helper <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="TUrlHelperResultTestBuilder">Type of URL helper result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithUrlHelperResult<TUrlHelperResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// has the same <see cref="IUrlHelper"/> as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type <see cref="IUrlHelper"/>.</param>
        /// <returns>The same URL helper <see cref="ActionResult"/> test builder.</returns>
        TUrlHelperResultTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type <see cref="IUrlHelper"/>.</typeparam>
        /// <returns>The same URL helper <see cref="ActionResult"/> test builder.</returns>
        TUrlHelperResultTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;
    }
}
