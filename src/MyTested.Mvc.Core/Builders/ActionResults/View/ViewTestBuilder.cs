namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System.Net;
    using Base;
    using Contracts.ActionResults.View;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing view result.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - <see cref="ViewResult"/> or <see cref="PartialViewResult"/>.</typeparam>
    public class ViewTestBuilder<TViewResult>
        : BaseTestBuilderWithViewFeature<TViewResult>, IAndViewTestBuilder
        where TViewResult : ActionResult
    {
        private string viewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder{TViewResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="viewType">View type name.</param>
        public ViewTestBuilder(
            ControllerTestContext testContext,
            string viewType)
            : base(testContext)
        {
            this.viewType = viewType;
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <inheritdoc />
        public IAndViewTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.ActionResult,
                statusCode,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithContentType(string contentType)
        {
            ContentTypeValidator.ValidateContentType(
                this.ActionResult,
                contentType,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);

        /// <inheritdoc />
        public IAndViewTestBuilder WithViewEngine(IViewEngine viewEngine)
        {
            var actualViewEngine = this.GetViewEngine();
            if (viewEngine != actualViewEngine)
            {
                this.ThrowNewViewResultAssertionException(
                    "ViewEngine",
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndViewTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine
        {
            var actualViewEngine = this.GetViewEngine();

            if (actualViewEngine == null
                || Reflection.AreDifferentTypes(typeof(TViewEngine), actualViewEngine.GetType()))
            {
                this.ThrowNewViewResultAssertionException(
                    "ViewEngine",
                    $"to be of {typeof(TViewEngine).Name} type",
                    $"instead received {actualViewEngine.GetName()}");
            }

            return this;
        }

        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new view result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed..</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewViewResultAssertionException(propertyName, expectedValue, actualValue);

        /// <summary>
        /// Gets the view engine instance from a view result.
        /// </summary>
        /// <returns>Type of IViewEngine.</returns>
        protected IViewEngine GetViewEngine()
        {
            IViewEngine viewEngine = null;
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                viewEngine = this.GetActionResultAsDynamic().ViewEngine;
            });

            return viewEngine;
        }

        /// <summary>
        /// Throws new ViewResultAssertionException.
        /// </summary>
        /// <param name="propertyName">Failed property.</param>
        /// <param name="expectedValue">Expected property value.</param>
        /// <param name="actualValue">Actual property value.</param>
        protected void ThrowNewViewResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ViewResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} result {3} {4}, but {5}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    this.viewType,
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
