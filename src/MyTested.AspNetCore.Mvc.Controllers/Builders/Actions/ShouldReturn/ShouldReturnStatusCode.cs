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
        public IAndStatusCodeTestBuilder StatusCode()
        {
            return this.GetStatusCodeTestBuilder();
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder StatusCode(int statusCode)
        {
            return this.StatusCode((HttpStatusCode)statusCode);
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder StatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.ActionResult,
                statusCode,
                this.ThrowNewStatusCodeResultAssertionException);

            return this.GetStatusCodeTestBuilder();
        }

        private IAndStatusCodeTestBuilder GetStatusCodeTestBuilder()
        {
            if (this.ActionResult is StatusCodeResult)
            {
                InvocationResultValidator.ValidateInvocationResultType<StatusCodeResult>(this.TestContext);
                return new StatusCodeTestBuilder<StatusCodeResult>(this.TestContext);
            }

            InvocationResultValidator.ValidateInvocationResultType<ObjectResult>(this.TestContext);
            return new StatusCodeTestBuilder<ObjectResult>(this.TestContext);
        }

        private void ThrowNewStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue)
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
