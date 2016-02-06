namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System.Net;
    using Contracts.Base;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing HttpStatusCodeResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> HttpStatusCode()
        {
            this.ValidateActionReturnType<HttpStatusCodeResult>();
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult and is the same as provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> HttpStatusCode(int statusCode)
        {
            return this.HttpStatusCode((HttpStatusCode)statusCode);
        }

        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> HttpStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeResult = this.GetReturnObject<HttpStatusCodeResult>();

            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                statusCodeResult.StatusCode,
                this.ThrowNewHttpStatusCodeResultAssertionException);

            return this.NewAndProvideTestBuilder();
        }

        private void ThrowNewHttpStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpStatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP status code result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
