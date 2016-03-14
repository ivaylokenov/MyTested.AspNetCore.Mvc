namespace MyTested.Mvc.Builders.ActionResults.LocalRedirect
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Contracts.ActionResults.LocalRedirect;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
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
        /// Tests whether local redirect result URL passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the URL.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder ToUrl(Action<string> assertions)
        {
            assertions(this.ActionResult.Url);
            return this;
        }

        /// <summary>
        /// Tests whether local redirect result URL passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the URL.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder ToUrl(Func<string, bool> predicate)
        {
            var url = this.ActionResult.Url;
            if (!predicate(url))
            {
                this.ThrowNewRedirectResultAssertionException(
                    $"location ('{url}')",
                    "to pass the given predicate",
                    "but it failed");
            }

            return this;
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
        /// Tests whether local redirect result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper)
        {
            RouteActionResultValidator.ValidateUrlHelper(
                this.ActionResult,
                urlHelper,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether local redirect result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper
        {
            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                this.ActionResult,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether local redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same local redirect test builder.</returns>
        public IAndLocalRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        public IAndLocalRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        /// <summary>
        /// AndAlso method for better readability when chaining local redirect result tests.
        /// </summary>
        /// <returns>Local redirect result test builder.</returns>
        public ILocalRedirectTestBuilder AndAlso() => this;
        
        private IAndLocalRedirectTestBuilder ProcessRouteLambdaExpression<TController>(LambdaExpression actionCall)
        {
            RouteActionResultValidator.ValidateExpressionLink(
                this.TestContext,
                LinkGenerationTestContext.FromLocalRedirectResult(this.ActionResult),
                actionCall,
                this.ThrowNewRedirectResultAssertionException);

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
