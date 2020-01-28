namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Redirect;
    using Contracts.ActionResults.Redirect;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="RedirectResult"/>,
    /// <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Redirect() => this.Redirect(null);

        /// <inheritdoc />
        public IAndTestBuilder Redirect(Action<IRedirectTestBuilder> redirectTestBuilder)
        {
            if (this.ActionResult is RedirectToRouteResult)
            {
                return this.ValidateRedirectResult<RedirectToRouteResult>(redirectTestBuilder);
            }

            if (this.ActionResult is RedirectToActionResult)
            {
                return this.ValidateRedirectResult<RedirectToActionResult>(redirectTestBuilder);
            }

            return this.ValidateRedirectResult<RedirectResult>(redirectTestBuilder);
        }

        private IAndTestBuilder ValidateRedirectResult<TRedirectResult>(
            Action<IRedirectTestBuilder> redirectTestBuilder)
            where TRedirectResult : ActionResult
            => this.ValidateActionResult<TRedirectResult, IRedirectTestBuilder>(
                redirectTestBuilder,
                new RedirectTestBuilder<TRedirectResult>(this.TestContext));
    }
}
