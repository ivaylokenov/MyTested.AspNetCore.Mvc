namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Json;
    using Contracts.ActionResults.Json;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="JsonResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IJsonTestBuilder Json()
        {
            this.TestContext.ActionResult = this.GetReturnObject<JsonResult>();
            return new JsonTestBuilder(this.TestContext);
        }
    }
}
