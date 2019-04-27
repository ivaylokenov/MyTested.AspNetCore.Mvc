namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.StatusCode;
    using And;
    using Contracts.ActionResults.StatusCode;
    using Contracts.And;
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
        public IAndTestBuilder StatusCode() => this.StatusCode(null);

        /// <inheritdoc />
        public IAndTestBuilder StatusCode(Action<IStatusCodeTestBuilder> statusCodeTestBuilder)
        {
            var actualStatusCodeTestBuilder = this.GetStatusCodeTestBuilder();

            statusCodeTestBuilder?.Invoke(actualStatusCodeTestBuilder);

            return new AndTestBuilder(this.TestContext);
        }

        public IAndStatusCodeTestBuilder GetStatusCodeTestBuilder()
        {
            if (this.ActionResult is StatusCodeResult)
            {
                InvocationResultValidator.ValidateInvocationResultType<StatusCodeResult>(this.TestContext);
                return new StatusCodeTestBuilder<StatusCodeResult>(this.TestContext);
            }

            InvocationResultValidator.ValidateInvocationResultType<ObjectResult>(this.TestContext);
            return new StatusCodeTestBuilder<ObjectResult>(this.TestContext);
        }

        public void ThrowNewStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new StatusCodeResultAssertionException(string.Format(
                "When calling {0} action in {1} expected status code result {2} {3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                propertyName,
                expectedValue,
                actualValue));
    }
}
