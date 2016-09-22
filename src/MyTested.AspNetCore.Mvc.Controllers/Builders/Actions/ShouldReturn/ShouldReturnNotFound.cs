namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.HttpNotFound;
    using Contracts.ActionResults.NotFound;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndNotFoundTestBuilder NotFound()
        {
            if (this.ActionResult is NotFoundObjectResult)
            {
                return this.ReturnNotFoundTestBuilder<NotFoundObjectResult>();
            }

            return this.ReturnNotFoundTestBuilder<NotFoundResult>();
        }

        private IAndNotFoundTestBuilder ReturnNotFoundTestBuilder<TNotFoundResult>()
            where TNotFoundResult : ActionResult
        {
            InvocationResultValidator.ValidateInvocationResultType<TNotFoundResult>(this.TestContext);
            return new NotFoundTestBuilder<TNotFoundResult>(this.TestContext);
        }
    }
}
