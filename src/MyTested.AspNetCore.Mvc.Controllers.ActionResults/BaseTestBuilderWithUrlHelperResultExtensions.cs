namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithUrlHelperResult{TUrlHelperResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithUrlHelperResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// has the same <see cref="IUrlHelper"/> as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithUrlHelper">
        /// Instance of <see cref="IBaseTestBuilderWithUrlHelperResult{TUrlHelperResultTestBuilder}"/> type.
        /// </param>
        /// <param name="urlHelper">URL helper of type <see cref="IUrlHelper"/>.</param>
        /// <returns>The same URL helper <see cref="ActionResult"/> test builder.</returns>
        public static TUrlHelperResultTestBuilder WithUrlHelper<TUrlHelperResultTestBuilder>(
            this IBaseTestBuilderWithUrlHelperResult<TUrlHelperResultTestBuilder> baseTestBuilderWithUrlHelper,
            IUrlHelper urlHelper)
            where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithUrlHelper);

            RouteActionResultValidator.ValidateUrlHelper(
                actualBuilder.TestContext.MethodResult,
                urlHelper,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithUrlHelper">
        /// Instance of <see cref="IBaseTestBuilderWithUrlHelperResult{TUrlHelperResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same URL helper <see cref="ActionResult"/> test builder.</returns>
        public static TUrlHelperResultTestBuilder WithUrlHelperOfType<TUrlHelperResultTestBuilder, TUrlHelper>(
            this IBaseTestBuilderWithUrlHelperResult<TUrlHelperResultTestBuilder> baseTestBuilderWithUrlHelper)
            where TUrlHelper : IUrlHelper
            where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithUrlHelper);

            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                actualBuilder.TestContext.MethodResult,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        private static IBaseTestBuilderWithUrlHelperResultInternal<TUrlHelperResultTestBuilder>
            GetActualBuilder<TUrlHelperResultTestBuilder>(
                IBaseTestBuilderWithUrlHelperResult<TUrlHelperResultTestBuilder> baseTestBuilderWithUrlHelperResult)
            where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithUrlHelperResultInternal<TUrlHelperResultTestBuilder>)baseTestBuilderWithUrlHelperResult;
    }
}
