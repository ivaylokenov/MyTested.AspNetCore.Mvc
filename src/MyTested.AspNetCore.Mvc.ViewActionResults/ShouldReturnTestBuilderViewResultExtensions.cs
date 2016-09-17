namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.View;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.View;
    using Builders.Contracts.Actions;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="ViewResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the default view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder View<TActionResult>(this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder)
        {
            return shouldReturnTestBuilder.View(null);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder View<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string viewName)
        {
            var actualShouldReturnTestBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            var viewType = "view";

            var viewResult = InvocationResultValidator
                .GetInvocationResult<ViewResult>(actualShouldReturnTestBuilder.TestContext);
            
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                throw ViewResultAssertionException.ForNameEquality(
                    actualShouldReturnTestBuilder.TestContext.ExceptionMessagePrefix,
                    viewType,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder<ViewResult>(actualShouldReturnTestBuilder.TestContext, viewType);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            TModel model)
        {
            return shouldReturnTestBuilder.View(null, model);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided view name and deeply equal model object.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        public static IViewTestBuilder View<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string viewName,
            TModel model)
        {
            var viewTestBuilder = shouldReturnTestBuilder.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }
    }
}
