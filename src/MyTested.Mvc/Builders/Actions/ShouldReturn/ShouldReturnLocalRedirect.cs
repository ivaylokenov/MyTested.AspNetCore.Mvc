namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.LocalRedirect;
    using Contracts.ActionResults.LocalRedirect;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing LocalRedirectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is LocalRedirectResult.
        /// </summary>
        /// <returns>Local redirect test builder.</returns>
        public ILocalRedirectTestBuilder LocalRedirect()
        {
            var localRedirectResult = this.GetReturnObject<LocalRedirectResult>();
            return new LocalRedirectTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                localRedirectResult);
        }
    }
}
