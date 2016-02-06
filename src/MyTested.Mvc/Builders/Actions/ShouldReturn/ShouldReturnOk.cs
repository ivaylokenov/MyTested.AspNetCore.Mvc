namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Ok;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing HttpOkResult or HttpOkObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpOkResult or HttpOkObjectResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        public IOkTestBuilder Ok()
        {
            if (this.ActionResult is HttpOkObjectResult)
            {
                return this.ReturnHttpOkTestBuilder<HttpOkObjectResult>();
            }

            return this.ReturnHttpOkTestBuilder<HttpOkResult>();
        }
        
        private IOkTestBuilder ReturnHttpOkTestBuilder<THttpOkResult>()
            where THttpOkResult : ActionResult
        {
            var createdResult = this.GetReturnObject<THttpOkResult>();
            return new OkTestBuilder<THttpOkResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                createdResult);
        }
    }
}
