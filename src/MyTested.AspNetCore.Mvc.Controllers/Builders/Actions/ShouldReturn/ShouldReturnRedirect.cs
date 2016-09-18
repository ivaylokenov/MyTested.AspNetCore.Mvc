namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Redirect;
    using Contracts.ActionResults.Redirect;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndRedirectTestBuilder Redirect()
        {
            if (this.ActionResult is RedirectToRouteResult)
            {
                return this.ReturnRedirectTestBuilder<RedirectToRouteResult>();
            }

            if (this.ActionResult is RedirectToActionResult)
            {
                return this.ReturnRedirectTestBuilder<RedirectToActionResult>();
            }

            return this.ReturnRedirectTestBuilder<RedirectResult>();
        }

        private IAndRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
            where TRedirectResult : ActionResult
        {
            InvocationResultValidator.ValidateInvocationResultType<TRedirectResult>(this.TestContext);
            return new RedirectTestBuilder<TRedirectResult>(this.TestContext);
        }
    }
}
