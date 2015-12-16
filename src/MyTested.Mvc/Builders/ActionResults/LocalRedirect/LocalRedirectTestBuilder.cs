namespace MyTested.Mvc.Builders.ActionResults.LocalRedirect
{
    using Microsoft.AspNet.Mvc;
    using Base;
    using Contracts.ActionResults.LocalRedirect;
    using Contracts.Uris;
    using System;
    using Utilities.Validators;
    using Common.Extensions;
    using Exceptions;

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

        public IAndLocalRedirectTestBuilder To(string localUrl)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                localUrl,
                this.ThrowNewRedirectResultAssertionException);

            return this.To(uri);
        }

        public IAndLocalRedirectTestBuilder To(Uri localUrl)
        {
            LocationValidator.ValidateUri(
                this.ActionResult,
                localUrl.ToString(),
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        public IAndLocalRedirectTestBuilder To(Action<IUriTestBuilder> localUrlTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.ActionResult,
                localUrlTestBuilder,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

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
