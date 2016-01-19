namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Content;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;

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
            this.ValidateActionReturnType<ContentResult>();
            return new ContentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as ContentResult);
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
                        "When calling {0} action in {1} expected content result Content to be '{2}', but instead received '{3}'.",
                        this.ActionName,
                        this.Controller.GetName(),
                        content,
                        actualContent));
            }

            return new ContentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                contentResult);
        }
    }
}
