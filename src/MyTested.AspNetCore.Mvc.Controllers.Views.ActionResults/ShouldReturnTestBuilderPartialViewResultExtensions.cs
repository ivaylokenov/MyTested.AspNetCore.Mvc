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
    /// Contains <see cref="PartialViewResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderPartialViewResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="PartialViewResult"/> with the provided partial view name.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewName">Expected partial view name.</param>
        /// <returns>Test builder of <see cref="IAndViewTestBuilder"/> type.</returns>
        public static IAndViewTestBuilder PartialView<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string viewName)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;
            
            var viewType = "partial view";

            var viewResult = InvocationResultValidator
                .GetInvocationResult<PartialViewResult>(actualBuilder.TestContext);

            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                throw ViewResultAssertionException.ForNameEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    viewType,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder<PartialViewResult>(actualBuilder.TestContext, viewType);
        }
    }
}
