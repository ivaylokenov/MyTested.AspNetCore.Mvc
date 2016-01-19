namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.LocalRedirect;
    using Contracts.ActionResults.LocalRedirect;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing LocalRedirectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is LocalRedirectResult.
        /// </summary>
        /// <returns>Local redirect test builder.</returns>
        public ILocalRedirectTestBuilder LocalRedirect()
        {
            this.ValidateActionReturnType<LocalRedirectResult>();
            return new LocalRedirectTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as LocalRedirectResult);
        }
    }
}
