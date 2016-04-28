namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.BadRequest;
    using Contracts.ActionResults.BadRequest;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
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
