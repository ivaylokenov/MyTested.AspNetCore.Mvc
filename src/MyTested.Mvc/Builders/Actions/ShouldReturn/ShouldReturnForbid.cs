namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Forbid;
    using Contracts.ActionResults.Forbid;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="ForbidResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IForbidTestBuilder Forbid()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ForbidResult>();
            return new ForbidTestBuilder(this.TestContext);
        }
    }
}
