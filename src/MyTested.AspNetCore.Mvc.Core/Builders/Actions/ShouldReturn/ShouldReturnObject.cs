namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Object;
    using Contracts.ActionResults.Object;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IObjectTestBuilder Object()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ObjectResult>();
            return new ObjectTestBuilder(this.TestContext);
        }
    }
}
