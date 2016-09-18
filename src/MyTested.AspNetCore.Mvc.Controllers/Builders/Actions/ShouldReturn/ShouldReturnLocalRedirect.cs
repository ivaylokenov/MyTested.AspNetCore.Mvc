namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.LocalRedirect;
    using Contracts.ActionResults.LocalRedirect;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="LocalRedirectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndLocalRedirectTestBuilder LocalRedirect()
        {
            InvocationResultValidator.ValidateInvocationResultType<LocalRedirectResult>(this.TestContext);
            return new LocalRedirectTestBuilder(this.TestContext);
        }
    }
}
