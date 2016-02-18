namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.HttpNotFound;
    using Contracts.ActionResults.HttpNotFound;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing HttpNotFoundResult or HttpNotFoundObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpNotFoundResult or HttpNotFoundObjectResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IHttpNotFoundTestBuilder HttpNotFound()
        {
            if (this.ActionResult is HttpNotFoundObjectResult)
            {
                return this.ReturnHttpNotFoundTestBuilder<HttpNotFoundObjectResult>();
            }

            return this.ReturnHttpNotFoundTestBuilder<HttpNotFoundResult>();
        }

        private IHttpNotFoundTestBuilder ReturnHttpNotFoundTestBuilder<THttpNotFoundResult>()
            where THttpNotFoundResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<THttpNotFoundResult>();
            return new HttpNotFoundTestBuilder<THttpNotFoundResult>(this.TestContext);
        }
    }
}
