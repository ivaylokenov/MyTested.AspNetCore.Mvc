namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using Contracts.ViewComponentResults;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using ViewComponentResults;
    using Utilities.Validators;
    using Exceptions;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndViewTestBuilder View()
        {
            return this.View(null);
        }

        /// <inheritdoc />
        public IAndViewTestBuilder View(string viewName)
        {
            var viewResult = InvocationResultValidator
                .GetInvocationResult<ViewViewComponentResult>(this.TestContext);

            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                throw ViewViewComponentResultAssertionException.ForNameEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    viewName,
                    actualViewName);
            }

            return new ViewTestBuilder(this.TestContext);
        }
    }
}
