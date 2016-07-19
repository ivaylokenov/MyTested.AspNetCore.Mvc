namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System.Net;
    using ActionResults.StatusCode;
    using Contracts.ActionResults.StatusCode;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="StatusCodeResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IStatusCodeTestBuilder StatusCode()
        {
            return this.GetStatusCodeTestBuilder();
        }

        /// <inheritdoc />
        public IStatusCodeTestBuilder StatusCode(int statusCode)
        {
            return this.StatusCode((HttpStatusCode)statusCode);
        }

        /// <inheritdoc />
        public IStatusCodeTestBuilder StatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.ActionResult,
                statusCode,
                this.ThrowNewStatusCodeResultAssertionException);

            return this.GetStatusCodeTestBuilder();
        }

        private IStatusCodeTestBuilder GetStatusCodeTestBuilder()
        {
            if (this.ActionResult is StatusCodeResult)
            {
                this.TestContext.ActionResult = this.GetReturnObject<StatusCodeResult>();
                return new StatusCodeTestBuilder<StatusCodeResult>(this.TestContext);
            }

            this.TestContext.ActionResult = this.GetReturnObject<ObjectResult>();
            return new StatusCodeTestBuilder<ObjectResult>(this.TestContext);
        }
        
        private void ThrowNewStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new StatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected status code result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Component.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
