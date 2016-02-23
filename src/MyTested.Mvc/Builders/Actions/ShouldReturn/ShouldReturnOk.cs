namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Ok;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing OkResult or OkObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is OkResult or OkObjectResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        public IOkTestBuilder Ok()
        {
            if (this.ActionResult is OkObjectResult)
            {
                return this.ReturnOkTestBuilder<OkObjectResult>();
            }

            return this.ReturnOkTestBuilder<OkResult>();
        }
        
        private IOkTestBuilder ReturnOkTestBuilder<THttpOkResult>()
            where THttpOkResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<THttpOkResult>();
            return new OkTestBuilder<THttpOkResult>(this.TestContext);
        }
    }
}
