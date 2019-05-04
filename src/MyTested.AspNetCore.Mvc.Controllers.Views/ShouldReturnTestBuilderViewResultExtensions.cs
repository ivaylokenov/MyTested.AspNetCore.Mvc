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
    /// Contains <see cref="ViewResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the default view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder View<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder)
            => builder.View(null);

        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the default view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewTestBuilder">Builder for testing the view result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder View<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            Action<IViewTestBuilder> viewTestBuilder)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            return actualBuilder.ValidateActionResult<ViewResult, IViewTestBuilder>(
                viewTestBuilder,
                new ViewTestBuilder(actualBuilder.TestContext));
        }
    }
}
