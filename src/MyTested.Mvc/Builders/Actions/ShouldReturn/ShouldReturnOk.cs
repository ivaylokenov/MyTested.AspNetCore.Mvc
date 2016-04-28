namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Ok;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="OkResult"/> or <see cref="OkObjectResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
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
