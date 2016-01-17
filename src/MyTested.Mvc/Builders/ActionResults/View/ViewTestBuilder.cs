namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System;
    using Contracts.ActionResults.View;
    using Microsoft.AspNet.Mvc;
    using Models;
    using Contracts.Models;
    using System.Net;
    using Utilities.Validators;
    using Exceptions;
    using Utilities;
    using Microsoft.Net.Http.Headers;
    using Microsoft.AspNet.Mvc.ViewEngines;
    using Internal.Extensions;

    /// <summary>
    /// Used for testing view results.
    /// </summary>
    public class ViewTestBuilder<TViewResult>
        : BaseResponseModelTestBuilder<TViewResult>, IAndViewTestBuilder
    {
        private string viewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ViewTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TViewResult actionResult,
            string viewType)
            : base(controller, actionName, caughtException, actionResult)
        {
            this.viewType = viewType;
        }

        public IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model)
        {
            return this.WithResponseModel(model);
        }

        public IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>()
        {
            return this.WithResponseModelOfType<TModel>();
        }

        /// <summary>
        /// Tests whether view result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithStatusCode(int statusCode)
        {
            return this.WithStatusCode((HttpStatusCode)statusCode);
        }

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
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether view result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view test builder.</returns>
        public IAndViewTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            ContentTypeValidator.ValidateContentType(
                this.ActionResult,
                contentType,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

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
            if (Reflection.AreDifferentTypes(typeof(TViewEngine), actualViewEngine.GetType()))
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
        public IViewTestBuilder AndAlso()
        {
            return this;
        }

        private IViewEngine GetViewEngine()
        {
            IViewEngine viewEngine = null;
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                viewEngine = this.GetActionResultAsDynamic().ViewEngine;
            });

            return viewEngine;
        }

        private void ThrowNewViewResultAssertionException(string propertyName, string expectedViewName, string actualViewName)
        {
            throw new ViewResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} result {3} {4}, but {5}.",
                    this.ActionName,
                    this.Controller,
                    this.viewType,
                    propertyName,
                    ViewTestHelper.GetFriendlyViewName(expectedViewName),
                    ViewTestHelper.GetFriendlyViewName(actualViewName)));
        }
    }
}
