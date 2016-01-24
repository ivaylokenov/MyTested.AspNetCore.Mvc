namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Json;
    using Contracts.ActionResults.Json;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing JsonResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is JsonResult.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        public IJsonTestBuilder Json()
        {
            var jsonResult = this.GetReturnObject<JsonResult>();
            return new JsonTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                jsonResult);
        }
    }
}
