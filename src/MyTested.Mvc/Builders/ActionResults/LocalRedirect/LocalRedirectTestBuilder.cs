namespace MyTested.Mvc.Builders.ActionResults.LocalRedirect
{
    using System;
    using Base;
    using Contracts.ActionResults.LocalRedirect;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;
    using Internal.TestContexts;
    /// <summary>
    /// Used for testing local redirect result.
    /// </summary>
    public class LocalRedirectTestBuilder : BaseTestBuilderWithActionResult<LocalRedirectResult>,
        IAndLocalRedirectTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalRedirectTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="localRedirectResult">Result from the tested action.</param>
        public LocalRedirectTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Tests whether local redirect result is permanent.
        /// </summary>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder Permanent()
        {
            if (!this.ActionResult.Permanent)
            {
                this.ThrowNewRedirectResultAssertionException(
                    "to",
                    "be permanent",
                    "in fact it was not");
            }

            return this;
        }

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as string.
        /// </summary>
        /// <param name="localUrl">Expected URL as string.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder ToUrl(string localUrl)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                localUrl,
                this.ThrowNewRedirectResultAssertionException);

            return this.ToUrl(uri);
        }

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as URI.
        /// </summary>
        /// <param name="localUrl">Expected URL as URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder ToUrl(Uri localUrl)
        {
            LocationValidator.ValidateUri(
                this.ActionResult,
                localUrl.OriginalString,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining local redirect result tests.
        /// </summary>
        /// <returns>Local redirect result test builder.</returns>
        public ILocalRedirectTestBuilder AndAlso() => this;
        
        private void ThrowNewRedirectResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new RedirectResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected local redirect result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
