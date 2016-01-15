namespace MyTested.Mvc.Builders.ActionResults.Created
{
    using System;
    using System.Linq.Expressions;
    using Contracts.ActionResults.Created;
    using Contracts.Uris;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Models;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">Type of created result - CreatedAtActionResult or CreatedAtRouteResult.</typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseResponseModelTestBuilder<TCreatedResult>, IAndCreatedTestBuilder
    {
        private const string RouteName = "route name";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public CreatedTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TCreatedResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
        
        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewCreatedResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            LocationValidator.ValidateUri(
                this.ActionResult,
                location.OriginalString,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.ActionResult,
                uriTestBuilder,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }
        
        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        public ICreatedTestBuilder AndAlso()
        {
            return this;
        }

        private IAndCreatedTestBuilder AtRoute<TController>(LambdaExpression actionCall)
            where TController : Controller
        {
            return this;
        }

        private void ThrowNewCreatedResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new CreatedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected created result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
