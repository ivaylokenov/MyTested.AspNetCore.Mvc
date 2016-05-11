namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Forbid;
    using Contracts.ActionResults.Forbid;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="ForbidResult"/>.
    /// </summary>
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
