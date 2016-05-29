namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.BadRequest;
    using Contracts.ActionResults.BadRequest;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </content>
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
