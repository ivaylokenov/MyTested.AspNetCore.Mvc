namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            TModel model)
        {
            return builder.View(null, model);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string viewName,
            TModel model)
        {
            var viewTestBuilder = builder.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder PartialView<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            TModel model)
        {
            return builder.PartialView(null, model);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided partial view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected partial view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder PartialView<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string viewName,
            TModel model)
        {
            var viewTestBuilder = builder.PartialView(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
