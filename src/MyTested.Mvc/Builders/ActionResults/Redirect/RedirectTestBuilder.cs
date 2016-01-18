namespace MyTested.Mvc.Builders.ActionResults.Redirect
{
    using System;
    using System.Linq.Expressions;
    using Base;
    using Contracts.ActionResults.Redirect;
    using Contracts.Base;
    using Contracts.Uris;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    /// <typeparam name="TRedirectResult">Type of redirect result - RedirectResult, RedirectToAction or RedirectToRouteResult.</typeparam>
    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, IRedirectTestBuilder
    {
        private const string Location = "location";
        private const string RouteName = "route name";

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectTestBuilder{TRedirectResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="redirectResult">Result from the tested action.</param>
        public RedirectTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TRedirectResult redirectResult)
            : base(controller, actionName, caughtException, redirectResult)
        {
        }
        
        /// <summary>
        /// Tests whether redirect result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewRedirectResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException AtLocation(Uri location)
        {
            var redirrectResult = this.GetRedirectResult<RedirectResult>(Location);
            LocationValidator.ValidateUri(
                redirrectResult,
                location.OriginalString,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            var redirrectResult = this.GetRedirectResult<RedirectResult>(Location);
            LocationValidator.ValidateLocation(
                redirrectResult,
                uriTestBuilder,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller
        {
            return this.RedirectTo<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException To<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller
        {
            return this.RedirectTo<TController>(actionCall);
        }

        private TExpectedRedirectResult GetRedirectResult<TExpectedRedirectResult>(string containment)
            where TExpectedRedirectResult : class
        {
            var actualRedirectResult = this.ActionResult as TExpectedRedirectResult;
            if (actualRedirectResult == null)
            {
                this.ThrowNewRedirectResultAssertionException(
                    "to contain",
                    containment,
                    "it could not be found");
            }

            return actualRedirectResult;
        }

        private IBaseTestBuilderWithCaughtException RedirectTo<TController>(LambdaExpression actionCall)
            where TController : Controller
        {
            return this;
        }

        private void ThrowNewRedirectResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new RedirectResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected redirect result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
