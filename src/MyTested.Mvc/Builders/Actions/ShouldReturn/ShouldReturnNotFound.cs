namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.HttpNotFound;
    using Contracts.ActionResults.NotFound;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing NotFoundResult or NotFoundObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is NotFoundResult or NotFoundObjectResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public INotFoundTestBuilder NotFound()
        {
            if (this.ActionResult is NotFoundObjectResult)
            {
                return this.ReturnNotFoundTestBuilder<NotFoundObjectResult>();
            }

            return this.ReturnNotFoundTestBuilder<NotFoundResult>();
        }

        private INotFoundTestBuilder ReturnNotFoundTestBuilder<TNotFoundResult>()
            where TNotFoundResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TNotFoundResult>();
            return new NotFoundTestBuilder<TNotFoundResult>(this.TestContext);
        }
    }
}
