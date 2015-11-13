namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Microsoft.AspNet.Mvc;
    using Contracts.ActionResults.Content;
    using ActionResults.Content;

    /// <summary>
    /// Class containing methods for testing ContentResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ContentResult.
        /// </summary>
        /// <returns>Content result test builder.</returns>
        public IContentTestBuilder Content()
        {
            this.ValidateActionReturnType(typeof(ContentResult));
            return new ContentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as ContentResult);
        }
    }
}
