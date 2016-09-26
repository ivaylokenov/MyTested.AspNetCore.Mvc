namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Content;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="ContentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndContentTestBuilder Content()
        {
            InvocationResultValidator.ValidateInvocationResultType<ContentResult>(this.TestContext);
            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndContentTestBuilder Content(string content)
        {
            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(this.TestContext);
            var actualContent = contentResult.Content;

            if (content != contentResult.Content)
            {
                throw ContentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }
            
            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndContentTestBuilder Content(Action<string> assertions)
        {
            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(this.TestContext);
            var actualContent = contentResult.Content;

            assertions(actualContent);

            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndContentTestBuilder Content(Func<string, bool> predicate)
        {
            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(this.TestContext);
            var actualContent = contentResult.Content;

            if (!predicate(actualContent))
            {
                throw ContentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return new ContentTestBuilder(this.TestContext);
        }
    }
}
