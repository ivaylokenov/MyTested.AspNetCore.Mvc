namespace MyTested.AspNetCore.Mvc
{
    using Builders.Base;
    using Builders.Contracts.ActionResults.View;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IViewTestBuilder" />.
    /// </summary>
    public static class ViewTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <param name="viewEngine">View engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngine(
            this IViewTestBuilder viewTestBuilder,
            IViewEngine viewEngine)
        {
            var actualBuilder = GetActualBuilder(viewTestBuilder);

            var actualViewEngine = GetViewEngine(viewTestBuilder);

            if (viewEngine != actualViewEngine)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "ViewEngine",
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngineOfType<TViewEngine>(
            this IViewTestBuilder viewTestBuilder)
            where TViewEngine : IViewEngine
        {
            var actualBuilder = GetActualBuilder(viewTestBuilder);

            var actualViewEngine = GetViewEngine(viewTestBuilder);

            if (actualViewEngine == null
                || Reflection.AreDifferentTypes(typeof(TViewEngine), actualViewEngine.GetType()))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "ViewEngine",
                    $"to be of {typeof(TViewEngine).Name} type",
                    $"instead received {actualViewEngine.GetName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Gets the view engine instance from a view result.
        /// </summary>
        /// <returns>Type of <see cref="IViewEngine"/>.</returns>
        private static IViewEngine GetViewEngine(IViewTestBuilder viewTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithComponent)viewTestBuilder;

            IViewEngine viewEngine = null;
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                viewEngine = actualBuilder.TestContext.MethodResult.AsDynamic()?.ViewEngine;
            });

            return viewEngine;
        }

        private static IBaseTestBuilderWithViewFeatureResultInternal<IAndViewTestBuilder> GetActualBuilder(
            IViewTestBuilder viewTestBuilder)
            => (IBaseTestBuilderWithViewFeatureResultInternal<IAndViewTestBuilder>)viewTestBuilder;
    }
}
