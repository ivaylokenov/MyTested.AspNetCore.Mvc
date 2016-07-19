namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Content;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <content>
    /// Class containing methods for testing <see cref="ContentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IContentTestBuilder Content()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ContentResult>();
            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IContentTestBuilder Content(string content)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            if (content != contentResult.Content)
            {
                throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected content result to contain '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Component.GetName(),
                    content,
                    actualContent));
            }

            this.TestContext.ActionResult = contentResult;
            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IContentTestBuilder Content(Action<string> assertions)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            assertions(actualContent);

            return new ContentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IContentTestBuilder Content(Func<string, bool> predicate)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            if (!predicate(actualContent))
            {
                throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected content result ('{2}') to pass the given predicate, but it failed.",
                    this.ActionName,
                    this.Component.GetName(),
                    actualContent));
            }

            return new ContentTestBuilder(this.TestContext);
        }
    }
}
