namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Created;
    using Contracts.ActionResults.Created;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndCreatedTestBuilder Created()
        {
            if (this.ActionResult is CreatedAtActionResult)
            {
                return this.ReturnCreatedTestBuilder<CreatedAtActionResult>();
            }

            if (this.ActionResult is CreatedAtRouteResult)
            {
                return this.ReturnCreatedTestBuilder<CreatedAtRouteResult>();
            }

            return this.ReturnCreatedTestBuilder<CreatedResult>();
        }

        private IAndCreatedTestBuilder ReturnCreatedTestBuilder<TCreatedResult>()
            where TCreatedResult : ObjectResult
        {
            InvocationResultValidator.ValidateInvocationResultType<TCreatedResult>(this.TestContext);
            return new CreatedTestBuilder<TCreatedResult>(this.TestContext);
        }
    }
}
