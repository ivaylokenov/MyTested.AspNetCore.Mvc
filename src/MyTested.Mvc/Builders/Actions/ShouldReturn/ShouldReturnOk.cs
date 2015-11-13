namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Microsoft.AspNet.Mvc;
    using Contracts.ActionResults.Ok;
    using ActionResults.Ok;

    /// <summary>
    /// Class containing methods for testing HttpOkResult or HttpOkObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpOkResult or HttpOkObjectResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        public IOkTestBuilder Ok()
        {
            this.ValidateActionReturnType(typeof(HttpOkResult), typeof(HttpOkObjectResult));
            return new OkTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
