namespace MyTested.Mvc.Builders.ActionResults.LocalRedirect
{
    using System;
    using Base;
    using Contracts.ActionResults.LocalRedirect;
    using Contracts.Uris;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Utilities.Validators;

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
        /// <param name="actionResult">Result from the tested action.</param>
        public LocalRedirectTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            LocalRedirectResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
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
                    string.Empty,
                    "to be permanent",
                    "but in fact it was not");
            }

            return this;
        }

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as string.
        /// </summary>
        /// <param name="localUrl">Expected URL as string.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder To(string localUrl)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                localUrl,
                this.ThrowNewRedirectResultAssertionException);

            return this.To(uri);
        }

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as URI.
        /// </summary>
        /// <param name="localUrl">Expected URL as URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder To(Uri localUrl)
        {
            LocationValidator.ValidateUri(
                this.ActionResult,
                localUrl.ToString(),
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether local redirect result has specific URL provided by builder.
        /// </summary>
        /// <param name="localUrlTestBuilder">Builder for expected URL.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder To(Action<IUriTestBuilder> localUrlTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.ActionResult,
                localUrlTestBuilder,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining local redirect result tests.
        /// </summary>
        /// <returns>Local redirect result test builder.</returns>
        public ILocalRedirectTestBuilder AndAlso()
        {
            return this;
        }
        
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
