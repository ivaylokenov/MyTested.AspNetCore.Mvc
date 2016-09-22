namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.BadRequest;
    using Contracts.ActionResults.BadRequest;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndBadRequestTestBuilder BadRequest()
        {
            if (this.ActionResult is BadRequestObjectResult)
            {
                return this.ReturnBadRequestTestBuilder<BadRequestObjectResult>();
            }

            return this.ReturnBadRequestTestBuilder<BadRequestResult>();
        }

        private IAndBadRequestTestBuilder ReturnBadRequestTestBuilder<TBadRequestResult>()
            where TBadRequestResult : ActionResult
        {
            InvocationResultValidator.ValidateInvocationResultType<TBadRequestResult>(this.TestContext);
            return new BadRequestTestBuilder<TBadRequestResult>(this.TestContext);
        }
    }
}
