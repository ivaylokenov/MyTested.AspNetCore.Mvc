namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.LocalRedirect;
    using Contracts.ActionResults.LocalRedirect;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="LocalRedirectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public ILocalRedirectTestBuilder LocalRedirect()
        {
            this.TestContext.ActionResult = this.GetReturnObject<LocalRedirectResult>();
            return new LocalRedirectTestBuilder(this.TestContext);
        }
    }
}
