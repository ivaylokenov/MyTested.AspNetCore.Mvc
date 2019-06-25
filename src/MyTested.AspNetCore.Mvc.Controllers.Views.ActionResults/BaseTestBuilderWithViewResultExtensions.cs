namespace MyTested.AspNetCore.Mvc
{
    using Builders.Base;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithViewResult{TViewResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithViewResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// or the <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same view name as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithViewResult">
        /// Instance of <see cref="IBaseTestBuilderWithViewResult{TViewResultTestBuilder}"/> type.
        /// </param>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TViewResultTestBuilder WithName<TViewResultTestBuilder>(
            this IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult,
            string viewName)
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithViewResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualViewName = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    ?.ViewName as string;

                if (viewName != actualViewName)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "to",
                        $"be {TestHelper.GetFriendlyName(viewName)}",
                        $"instead received {TestHelper.GetFriendlyName(actualViewName)}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// or the <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithViewResult">
        /// Instance of <see cref="IBaseTestBuilderWithViewResult{TViewResultTestBuilder}"/> type.
        /// </param>
        /// <param name="viewEngine">Expected view engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TViewResultTestBuilder WithViewEngine<TViewResultTestBuilder>(
            this IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult,
            IViewEngine viewEngine) 
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithViewResult);

            var actualViewEngine = GetViewEngine(baseTestBuilderWithViewResult);

            if (viewEngine != actualViewEngine)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "engine",
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or
        /// the <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithViewResult">
        /// Instance of <see cref="IBaseTestBuilderWithViewResult{TViewResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TViewResultTestBuilder WithViewEngineOfType<TViewResultTestBuilder, TViewEngine>(
            this IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult)
            where TViewEngine : IViewEngine 
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithViewResult);

            var actualViewEngine = GetViewEngine(baseTestBuilderWithViewResult);

            if (actualViewEngine == null
                || Reflection.AreDifferentTypes(typeof(TViewEngine), actualViewEngine.GetType()))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "engine",
                    $"to be of {typeof(TViewEngine).Name} type",
                    $"instead received {actualViewEngine.GetName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Gets the view engine instance from a view result.
        /// </summary>
        /// <returns>Type of <see cref="IViewEngine"/>.</returns>
        private static IViewEngine GetViewEngine<TViewResultTestBuilder>(
            IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult) 
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = (BaseTestBuilderWithComponent)baseTestBuilderWithViewResult;

            IViewEngine viewEngine = null;
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                viewEngine = actualBuilder.TestContext.MethodResult.AsDynamic()?.ViewEngine;
            });

            return viewEngine;
        }

        private static IBaseTestBuilderWithViewFeatureResultInternal<TViewResultTestBuilder> 
            GetActualBuilder<TViewResultTestBuilder>(
                IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult) 
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult 
            => (IBaseTestBuilderWithViewFeatureResultInternal<TViewResultTestBuilder>)baseTestBuilderWithViewResult;
    }
}
