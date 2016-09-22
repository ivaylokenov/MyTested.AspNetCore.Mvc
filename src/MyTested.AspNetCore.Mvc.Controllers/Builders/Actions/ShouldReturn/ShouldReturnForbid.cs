namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Forbid;
    using Contracts.ActionResults.Forbid;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="ForbidResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndForbidTestBuilder Forbid()
        {
            InvocationResultValidator.ValidateInvocationResultType<ForbidResult>(this.TestContext);
            return new ForbidTestBuilder(this.TestContext);
        }
    }
}
