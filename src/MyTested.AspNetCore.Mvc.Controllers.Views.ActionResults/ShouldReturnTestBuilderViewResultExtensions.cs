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
        /// Tests whether the action result is <see cref="ViewResult"/> with the provided view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder View<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string viewName)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            var viewType = "view";

            var viewResult = InvocationResultValidator
                .GetInvocationResult<ViewResult>(actualBuilder.TestContext);
            
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                throw ViewResultAssertionException.ForNameEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    viewType,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder<ViewResult>(actualBuilder.TestContext, viewType);
        }
    }
}
