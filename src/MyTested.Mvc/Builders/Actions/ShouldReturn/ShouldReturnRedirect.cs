namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Redirect;
    using Contracts.ActionResults.Redirect;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing RedirectResult, RedirectToActionResult or RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is RedirectResult, RedirectToActionResult or RedirectToRouteResult.
        /// </summary>
        /// <returns>Redirect test builder.</returns>
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
            var redirectResult = this.GetReturnObject<TRedirectResult>();
            return new RedirectTestBuilder<TRedirectResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                redirectResult);
        }
    }
}
