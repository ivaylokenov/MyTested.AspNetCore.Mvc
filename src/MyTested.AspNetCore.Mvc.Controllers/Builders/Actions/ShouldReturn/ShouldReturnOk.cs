namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Ok;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="OkResult"/> or <see cref="OkObjectResult"/>.
    /// </content>
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
            InvocationResultValidator.ValidateInvocationResultType<THttpOkResult>(this.TestContext);
            return new OkTestBuilder<THttpOkResult>(this.TestContext);
        }
    }
}
