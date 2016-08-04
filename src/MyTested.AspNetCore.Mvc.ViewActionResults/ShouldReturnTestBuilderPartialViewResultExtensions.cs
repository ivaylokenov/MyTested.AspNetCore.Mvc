namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.View;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains <see cref="PartialViewResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderPartialViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the default view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder PartialView<TActionResult>(this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder)
        {
            return shouldReturnTestBuilder.PartialView(null);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided partial view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected partial view name.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder PartialView<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string viewName)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            var viewType = "partial view";
            var viewResult = actualShouldReturnTestBuilder.GetReturnObject<PartialViewResult>();
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                ViewFeaturesThrow.NewViewResultAssertionException(
                    actualShouldReturnTestBuilder,
                    viewType,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder<PartialViewResult>(actualShouldReturnTestBuilder.TestContext, viewType);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder PartialView<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            TModel model)
        {
            return shouldReturnTestBuilder.PartialView(null, model);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided partial view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected partial view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder PartialView<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string viewName, TModel model)
        {
            var viewTestBuilder = shouldReturnTestBuilder.PartialView(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
