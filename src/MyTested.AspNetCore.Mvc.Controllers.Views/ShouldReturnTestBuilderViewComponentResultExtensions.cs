namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.View;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains <see cref="ViewComponentResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderViewComponentResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ViewComponentResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ViewComponent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder)
            => builder.ViewComponent(null);

        /// <summary>
        /// Tests whether the action result is <see cref="ViewComponentResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewComponentTestBuilder">Builder for testing the view component result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ViewComponent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            Action<IViewComponentTestBuilder> viewComponentTestBuilder)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            return actualBuilder.ValidateActionResult<ViewComponentResult, IViewComponentTestBuilder>(
                viewComponentTestBuilder,
                new ViewComponentTestBuilder(actualBuilder.TestContext));
        }
    }
}
