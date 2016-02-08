namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System;
    using System.Net;
    using Base;
    using Contracts.ActionResults.View;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing view results.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - ViewResult or PartialViewResult.</typeparam>
    public class ViewTestBuilder<TViewResult>
        : BaseTestBuilderWithViewFeature<TViewResult>, IAndViewTestBuilder
        where TViewResult : ActionResult
    {
        private string viewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder{TViewResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="viewResult">Result from the tested action.</param>
        /// <param name="viewType">View type name.</param>
        public ViewTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TViewResult viewResult,
            string viewType)
            : base(controller, actionName, caughtException, viewResult)
        {
            this.viewType = viewType;
        }

        /// <summary>
        /// Tests whether view result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <summary>
        /// Tests whether view result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.ActionResult,
                statusCode,
                this.ThrowNewViewResultAssertionException);

            return this;
        }
        
        /// <summary>
        /// Tests whether view result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithContentType(string contentType)
        {
            ContentTypeValidator.ValidateContentType(
                this.ActionResult,
                contentType,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether view result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);

        /// <summary>
        /// Tests whether view result has the same view engine as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type IViewEngine.</param>
        /// <returns>The same view test builder.</returns>
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

        /// <summary>
        /// Tests whether view result has the same view engine type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type IViewEngine.</typeparam>
        /// <returns>The same view test builder.</returns>
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

        /// <summary>
        /// AndAlso method for better readability when chaining view result tests.
        /// </summary>
        /// <returns>View result test builder.</returns>
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
