namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System.Net;
    using Contracts.Base;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing <see cref="StatusCodeResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode()
        {
            this.ValidateActionReturnType<StatusCodeResult>();
            return this.NewAndProvideTestBuilder();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode(int statusCode)
        {
            return this.StatusCode((HttpStatusCode)statusCode);
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode)
        {
            var statusCodeResult = this.GetReturnObject<StatusCodeResult>();

            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                statusCodeResult.StatusCode,
                this.ThrowNewHttpStatusCodeResultAssertionException);

            return this.NewAndProvideTestBuilder();
        }

        private void ThrowNewHttpStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new StatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected status code result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
