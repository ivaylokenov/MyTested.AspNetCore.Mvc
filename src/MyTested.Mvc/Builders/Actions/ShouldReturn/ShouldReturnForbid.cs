namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Forbid;
    using Contracts.ActionResults.Forbid;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing ForbidResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ForbidResult.
        /// </summary>
        /// <returns>Forbid test builder.</returns>
        public IForbidTestBuilder Forbid()
        {
            this.ValidateActionReturnType<ForbidResult>();
            return new ForbidTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as ForbidResult);
        }
    }
}
