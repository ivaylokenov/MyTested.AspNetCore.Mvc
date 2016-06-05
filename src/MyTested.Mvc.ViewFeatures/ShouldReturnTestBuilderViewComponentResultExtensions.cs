namespace MyTested.Mvc
{
    using System;
    using Builders.ActionResults.View;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;

    /// <summary>
    /// Contains <see cref="ViewComponentResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderViewComponentResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ViewComponentResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        public static IViewComponentTestBuilder ViewComponent<TActionResult>(this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            actualShouldReturnTestBuilder.TestContext.ActionResult = actualShouldReturnTestBuilder.GetReturnObject<ViewComponentResult>();

            return new ViewComponentTestBuilder(actualShouldReturnTestBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewComponentResult"/> with the provided view component name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewComponentName">Expected view component name.</param>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        public static IViewComponentTestBuilder ViewComponent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string viewComponentName)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            var viewComponentResult = actualShouldReturnTestBuilder.GetReturnObject<ViewComponentResult>();
            var actualViewComponentName = viewComponentResult.ViewComponentName;

            if (viewComponentName != actualViewComponentName)
            {
                ViewFeaturesThrow.NewViewResultAssertionException(
                    actualShouldReturnTestBuilder,
                    "view component",
                    viewComponentName,
                    actualViewComponentName);
            }

            actualShouldReturnTestBuilder.TestContext.ActionResult = viewComponentResult;
            return new ViewComponentTestBuilder(actualShouldReturnTestBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewComponentResult"/> with the provided view component type.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewComponentType">Expected view component type.</param>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        public static IViewComponentTestBuilder ViewComponent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Type viewComponentType)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            var viewComponentResult = actualShouldReturnTestBuilder.GetReturnObject<ViewComponentResult>();
            var actualViewComponentType = viewComponentResult.ViewComponentType;

            if (viewComponentType != actualViewComponentType)
            {
                ViewFeaturesThrow.NewViewResultAssertionException(
                    actualShouldReturnTestBuilder,
                    "view component",
                    viewComponentType.ToFriendlyTypeName(),
                    actualViewComponentType.ToFriendlyTypeName());
            }

            actualShouldReturnTestBuilder.TestContext.ActionResult = viewComponentResult;
            return new ViewComponentTestBuilder(actualShouldReturnTestBuilder.TestContext);
        }
    }
}
