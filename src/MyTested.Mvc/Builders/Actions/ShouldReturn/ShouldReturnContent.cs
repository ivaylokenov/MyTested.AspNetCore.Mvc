namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Content;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <summary>
    /// Class containing methods for testing ContentResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ContentResult.
        /// </summary>
        /// <returns>Content test builder.</returns>
        public IContentTestBuilder Content()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ContentResult>();
            return new ContentTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Tests whether action result is ContentResult with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Content result test builder.</returns>
        public IContentTestBuilder Content(string content)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            if (content != contentResult.Content)
            {
                throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected content result to contain '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Controller.GetName(),
                    content,
                    actualContent));
            }

            this.TestContext.ActionResult = contentResult;
            return new ContentTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Tests whether content result passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the content.</param>
        /// <returns>Content result test builder.</returns>
        public IContentTestBuilder Content(Action<string> assertions)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            assertions(actualContent);
        
            return new ContentTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Tests whether content result passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Content result test builder.</returns>
        public IContentTestBuilder Content(Func<string, bool> predicate)
        {
            var contentResult = this.GetReturnObject<ContentResult>();
            var actualContent = contentResult.Content;

            if (!predicate(actualContent))
            {
                throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected content result ('{2}') to pass the given predicate, but it failed.",
                    this.ActionName,
                    this.Controller.GetName(),
                    actualContent));
            }

            return new ContentTestBuilder(this.TestContext);
        }
}
}
