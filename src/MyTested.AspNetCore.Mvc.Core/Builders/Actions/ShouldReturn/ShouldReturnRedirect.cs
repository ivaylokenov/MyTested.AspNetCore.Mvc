namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Redirect;
    using Contracts.ActionResults.Redirect;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IRedirectTestBuilder Redirect()
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

        private IRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
            where TRedirectResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TRedirectResult>();
            return new RedirectTestBuilder<TRedirectResult>(this.TestContext);
        }
    }
}
