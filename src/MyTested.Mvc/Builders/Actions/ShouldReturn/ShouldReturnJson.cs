namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Microsoft.AspNet.Mvc;
    using Contracts.ActionResults.Json;
    using ActionResults.Json;

    /// <summary>
    /// Class containing methods for testing JsonResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is JsonResult.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        public IJsonTestBuilder Json()
        {
            this.ResultOfType<JsonResult>();
            return new JsonTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as JsonResult);
        }
    }
}
