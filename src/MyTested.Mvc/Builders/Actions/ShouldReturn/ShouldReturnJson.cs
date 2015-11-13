namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Microsoft.AspNet.Mvc;
    using Contracts.ActionResults.Json;
    using ActionResults.Json;

    /// <summary>
    /// Class containing methods for testing JSON Result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        public IJsonTestBuilder Json()
        {
            this.ResultOfType(typeof(JsonResult));
            return new JsonTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as JsonResult);
        }
    }
}
