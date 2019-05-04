namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Redirect
{
    using System;
    using Base;
    using Contracts.ActionResults.Redirect;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    /// <typeparam name="TRedirectResult">Type of redirect result - <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.</typeparam>
    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, 
        IAndRedirectTestBuilder,
        IBaseTestBuilderWithRedirectResultInternal<IAndRedirectTestBuilder>,
        IBaseTestBuilderWithRouteValuesResultInternal<IAndRedirectTestBuilder>
        where TRedirectResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectTestBuilder{TRedirectResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public RedirectTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the redirect result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndRedirectTestBuilder"/> type.</value>
        public IAndRedirectTestBuilder ResultTestBuilder => this;

        public bool IncludeCountCheck { get; set; } = true;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<RedirectResult> assertions)
        {
            this.ValidateRedirectResult<RedirectResult>();
            return this.Passing<RedirectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<RedirectResult, bool> predicate)
        {
            this.ValidateRedirectResult<RedirectResult>();
            return this.Passing<RedirectResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<RedirectToRouteResult> assertions)
        {
            this.ValidateRedirectResult<RedirectToRouteResult>();
            return this.Passing<RedirectToRouteResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<RedirectToRouteResult, bool> predicate)
        {
            this.ValidateRedirectResult<RedirectToRouteResult>();
            return this.Passing<RedirectToRouteResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<RedirectToActionResult> assertions)
        {
            this.ValidateRedirectResult<RedirectToActionResult>();
            return this.Passing<RedirectToActionResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<RedirectToActionResult, bool> predicate)
        {
            this.ValidateRedirectResult<RedirectToActionResult>();
            return this.Passing<RedirectToActionResult>(predicate);
        }

        /// <inheritdoc />
        public IRedirectTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="RedirectResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new RedirectResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "redirect",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateRedirectResult<TResult>()
            where TResult : ActionResult
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(TResult);

            if (actualResultType != expectedResultType)
            {
                throw new RedirectResultAssertionException(string.Format(
                    "{0} redirect result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
