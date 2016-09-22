namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using And;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Utilities.Validators;
    using Exceptions;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Content()
        {
            InvocationResultValidator.ValidateInvocationResultType<ContentViewComponentResult>(this.TestContext);
            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder Content(string content)
        {
            var actualContent = this.GetActualContent();

            if (content != actualContent)
            {
                throw ContentViewComponentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder Content(Action<string> assertions)
        {
            var actualContent = this.GetActualContent();

            assertions(actualContent);

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder Content(Func<string, bool> predicate)
        {
            var actualContent = this.GetActualContent();

            if (!predicate(actualContent))
            {
                throw ContentViewComponentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return new AndTestBuilder(this.TestContext);
        }

        private string GetActualContent()
            => InvocationResultValidator
                .GetInvocationResult<ContentViewComponentResult>(this.TestContext)
                .Content;
    }
}
