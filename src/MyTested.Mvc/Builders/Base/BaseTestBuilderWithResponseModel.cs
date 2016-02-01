namespace MyTested.Mvc.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contracts.Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Models;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all response model test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithResponseModel<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IBaseTestBuilderWithResponseModel
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected response model {2} to be the given model, but in fact it was a different.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected response model to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithResponseModel{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseTestBuilderWithResponseModel(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
            this.ErrorMessageFormat = ErrorMessage;
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage;
        }

        /// <summary>
        /// Gets or sets the error message format for the response model assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string ErrorMessageFormat { get; set; }

        /// <summary>
        /// Gets or sets the error message format for the response model type assertions.
        /// </summary>
        /// <value>String value.</value>
        protected string OfTypeErrorMessageFormat { get; set; }

        /// <summary>
        /// Tests whether certain type of response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
        {
            var actualResponseDataType = this.GetReturnType();
            var expectedResponseDataType = typeof(TResponseModel);

            var responseDataTypeIsAssignable = Reflection.AreAssignable(
                    expectedResponseDataType,
                    actualResponseDataType);

            if (!responseDataTypeIsAssignable)
            {
                throw new ResponseModelAssertionException(string.Format(
                    this.OfTypeErrorMessageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TResponseModel).ToFriendlyTypeName(),
                    actualResponseDataType.ToFriendlyTypeName()));
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.GetActualModel<TResponseModel>());
        }

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
        {
            this.WithResponseModelOfType<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            if (Reflection.AreNotDeeplyEqual(expectedModel, actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                            this.ErrorMessageFormat,
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        /// <summary>
        /// Tests whether action result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        protected void ValidateStatusCode(int statusCode)
        {
            this.ValidateStatusCode((HttpStatusCode)statusCode);
        }

        /// <summary>
        /// Tests whether action result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        protected void ValidateStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.ActionResult,
                statusCode,
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Expected content type as string.</param>
        protected void ValidateContainingOfContentType(string contentType)
        {
            this.ValidateContainingOfContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether action result contains the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Expected content type as MediaTypeHeaderValue.</param>
        protected void ValidateContainingOfContentType(MediaTypeHeaderValue contentType)
        {
            ContentTypeValidator.ValidateContainingOfContentType(
                this.ActionResult,
                contentType,
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains the same content types as the provided ones.
        /// </summary>
        /// <param name="contentTypes">Expected content types as enumerable of strings.</param>
        protected void ValidateContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes.Select(ct => new MediaTypeHeaderValue(ct)));
        }

        /// <summary>
        /// Tests whether action result contains the same content types as the provided ones.
        /// </summary>
        /// <param name="contentTypes">Expected content types as string parameters.</param>
        protected void ValidateContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes.AsEnumerable());
        }

        /// <summary>
        /// Tests whether action result contains the same content types as the provided ones.
        /// </summary>
        /// <param name="contentTypes">Expected content types as enumerable of MediaTypeHeaderValue.</param>
        protected void ValidateContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            ContentTypeValidator.ValidateContentTypes(
                this.ActionResult,
                contentTypes,
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains the same content types as the provided ones.
        /// </summary>
        /// <param name="contentTypes">Expected content types as MediaTypeHeaderValue parameters.</param>
        protected void ValidateContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes.AsEnumerable());
        }

        /// <summary>
        /// Tests whether action result contains the same output formatter as the provided one.
        /// </summary>
        /// <param name="outputFormatter">Expected instance of IOutputFormatter.</param>
        protected void ValidateContainingOfOutputFormatter(IOutputFormatter outputFormatter)
        {
            OutputFormatterValidator.ValidateContainingOfOutputFormatter(
                this.GetObjectResult(),
                outputFormatter,
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains type of output formatter as the provided one.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Expected type of IOutputFormatter.</typeparam>
        protected void ValidateContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            OutputFormatterValidator.ValidateContainingOutputFormatterOfType<TOutputFormatter>(
                this.GetObjectResult(),
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains the same output formatters as the provided ones.
        /// </summary>
        /// <param name="outputFormatters">Expected enumerable of IOutputFormatter.</param>
        protected void ValidateOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            OutputFormatterValidator.ValidateOutputFormatters(
                this.GetObjectResult(),
                outputFormatters,
                this.ThrowNewFailedValidationException);
        }

        /// <summary>
        /// Tests whether action result contains the same output formatters as the provided ones.
        /// </summary>
        /// <param name="outputFormatters">Expected IOutputFormatter parameters.</param>
        protected void ValidateOutputFormatters(params IOutputFormatter[] outputFormatters)
            => this.ValidateOutputFormatters(outputFormatters.AsEnumerable());

        /// <summary>
        /// When overridden in a derived class, it will be used to throw failed validation exception.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected abstract void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue);

        private TResponseModel GetActualModel<TResponseModel>()
        {
            try
            {
                return (TResponseModel)this.GetActualModel();
            }
            catch (InvalidCastException)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model to be a {2}, but instead received null.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TResponseModel).ToFriendlyTypeName()));
            }
        }

        private ObjectResult GetObjectResult()
        {
            var objectResult = this.ActionResult as ObjectResult;
            if (objectResult == null)
            {
                throw new ActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to inherit from ObjectResult, but it did not.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return objectResult;
        }

        private object GetActualModel()
        {
            if (this.ActionResult is ObjectResult)
            {
                return (this.ActionResult as ObjectResult)?.Value;
            }

            if (this.ActionResult is JsonResult)
            {
                return (this.ActionResult as JsonResult)?.Value;
            }

            if (this.ActionResult is ViewResult)
            {
                return (this.ActionResult as ViewResult)?.Model;
            }

            if (this.ActionResult is PartialViewResult)
            {
                return (this.ActionResult as PartialViewResult)?.ViewData?.Model;
            }

            return null;
        }

        private Type GetReturnType()
        {
            if (this.ActionResult is ObjectResult)
            {
                var declaredType = (this.ActionResult as ObjectResult)?.DeclaredType;
                if (declaredType != null)
                {
                    return declaredType;
                }
            }

            return this.GetActualModel()?.GetType();
        }
    }
}
