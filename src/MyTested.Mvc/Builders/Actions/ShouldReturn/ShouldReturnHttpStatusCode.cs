namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System.Net;
    using Internal.Extensions;
    using Contracts.Base;
    using Exceptions;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing HttpStatusCodeResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode()
        {
            this.ResultOfType<HttpStatusCodeResult>();
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode)
        {
            var statusCodeResult = this.GetReturnObject<HttpStatusCodeResult>();
            var actualStatusCode = statusCodeResult.StatusCode;
            if (statusCodeResult.StatusCode != actualStatusCode)
            {
                throw new HttpStatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetName(),
                    (int)statusCode,
                    statusCode,
                    actualStatusCode,
                    (HttpStatusCode)actualStatusCode));
            }

            return this.NewAndProvideTestBuilder();
        }
    }
}
