namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing HttpNotFoundResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpNotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotFound()
        {
            this.ResultOfType<HttpNotFoundResult>();
            return this.NewAndProvideTestBuilder(); // TODO: there are too types of HttpNotFound
        }
    }
}
