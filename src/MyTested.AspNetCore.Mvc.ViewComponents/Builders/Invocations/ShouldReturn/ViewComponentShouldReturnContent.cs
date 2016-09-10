namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Utilities.Validators;
    using Exceptions;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content()
        {
            InvocationResultValidator.ValidateInvocationResultType<ContentViewComponentResult>(this.TestContext);
            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(string content)
        {
            var actualContent = this.GetActualContent();

            if (content != actualContent)
            {
                throw ContentViewComponentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Action<string> assertions)
        {
            var actualContent = this.GetActualContent();

            assertions(actualContent);

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Func<string, bool> predicate)
        {
            var actualContent = this.GetActualContent();

            if (!predicate(actualContent))
            {
                throw ContentViewComponentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        private string GetActualContent()
            => InvocationResultValidator
                .GetInvocationResult<ContentViewComponentResult>(this.TestContext)
                .Content;
    }
}
