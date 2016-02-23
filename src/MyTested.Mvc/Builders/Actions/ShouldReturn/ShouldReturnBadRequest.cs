namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.BadRequest;
    using Contracts.ActionResults.BadRequest;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing BadRequestResult or BadRequestObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is BadRequestResult or BadRequestObjectResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        public IBadRequestTestBuilder BadRequest()
        {
            if (this.ActionResult is BadRequestObjectResult)
            {
                return this.ReturnBadRequestTestBuilder<BadRequestObjectResult>();
            }

            return this.ReturnBadRequestTestBuilder<BadRequestResult>();
        }

        private IBadRequestTestBuilder ReturnBadRequestTestBuilder<TBadRequestResult>()
            where TBadRequestResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TBadRequestResult>();
            return new BadRequestTestBuilder<TBadRequestResult>(this.TestContext);
        }
    }
}
