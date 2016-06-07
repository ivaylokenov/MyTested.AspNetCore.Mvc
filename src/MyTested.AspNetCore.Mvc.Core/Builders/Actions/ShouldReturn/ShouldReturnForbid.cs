namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Forbid;
    using Contracts.ActionResults.Forbid;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ForbidResult"/>.
    /// </content>
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
