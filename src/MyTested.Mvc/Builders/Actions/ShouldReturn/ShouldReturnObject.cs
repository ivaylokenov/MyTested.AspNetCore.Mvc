namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Object;
    using Contracts.ActionResults.Object;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="ObjectResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
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
