namespace MyTested.Mvc.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contracts.Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Models;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with response model.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithResponseModel<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IBaseTestBuilderWithResponseModel
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected response model {2} to be the given model, but in fact it was a different one.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected response model to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithResponseModel{TActionResult}" /> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithResponseModel(ControllerTestContext testContext)
            : base(testContext)
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

        /// <inheritdoc />
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

            this.TestContext.Model = this.GetActualModel<TResponseModel>();
            return new ModelDetailsTestBuilder<TResponseModel>(this.TestContext);
        }

        /// <inheritdoc />
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

            this.TestContext.Model = actualModel;
            return new ModelDetailsTestBuilder<TResponseModel>(this.TestContext);
        }

        /// <summary>
        /// Tests whether action result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        protected void ValidateStatusCode(int statusCode)
        {
            this.ValidateStatusCode((HttpStatusCode)statusCode);
        }

        /// <summary>
        /// Tests whether action result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
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
        /// <param name="contentType">Expected content type as <see cref="MediaTypeHeaderValue"/>.</param>
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
        /// <param name="contentTypes">Expected content types as collection of strings.</param>
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
        /// <param name="contentTypes">Expected content types as collection of <see cref="MediaTypeHeaderValue"/>.</param>
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
        /// <param name="contentTypes">Expected content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        protected void ValidateContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes.AsEnumerable());
        }

        /// <summary>
        /// Tests whether action result contains the same output formatter as the provided one.
        /// </summary>
        /// <param name="outputFormatter">Expected instance of <see cref="IOutputFormatter"/>.</param>
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
        /// <typeparam name="TOutputFormatter">Expected type of <see cref="IOutputFormatter"/>.</typeparam>
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
        /// <param name="outputFormatters">Expected collection of <see cref="IOutputFormatter"/>.</param>
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
        /// <param name="outputFormatters">Expected <see cref="IOutputFormatter"/> parameters.</param>
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
                return (this.ActionResult as ObjectResult).Value;
            }

            if (this.ActionResult is JsonResult)
            {
                return (this.ActionResult as JsonResult).Value;
            }

            if (this.ActionResult is ViewResult)
            {
                return (this.ActionResult as ViewResult).Model;
            }

            if (this.ActionResult is PartialViewResult)
            {
                return (this.ActionResult as PartialViewResult).ViewData?.Model;
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
