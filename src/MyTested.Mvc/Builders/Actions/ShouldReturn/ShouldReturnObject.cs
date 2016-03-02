namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Object;
    using Contracts.ActionResults.Object;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing ObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ObjectResult.
        /// </summary>
        /// <returns>Object result test builder.</returns>
        public IObjectTestBuilder Object()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ObjectResult>();
            return new ObjectTestBuilder(this.TestContext);
        }
    }
}
