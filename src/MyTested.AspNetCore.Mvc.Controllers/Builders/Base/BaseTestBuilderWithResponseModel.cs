namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with response model.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithResponseModel<TActionResult> : BaseTestBuilderWithResponseModel
        where TActionResult : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithResponseModel{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithResponseModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        protected TActionResult ActionResult => this.TestContext.MethodResultAs<TActionResult>();
        
        public override object GetActualModel()
        {
            return (this.TestContext.MethodResult as ObjectResult)?.Value;
        }

        public override Type GetModelReturnType()
        {
            if (this.TestContext.MethodResult is ObjectResult)
            {
                var declaredType = (this.TestContext.MethodResult as ObjectResult).DeclaredType;
                if (declaredType != null)
                {
                    return declaredType;
                }
            }

            return this.GetActualModel()?.GetType();
        }

        public override void ValidateNoModel()
        {
            if (this.GetActualModel() != null)
            {
                this.ThrowNewResponseModelAssertionException();
            }
        }

        protected void WithNoModel<TExpectedActionResult>()
            where TExpectedActionResult : ActionResult
        {
            var actualResult = this.TestContext.MethodResult as TExpectedActionResult;
            if (actualResult == null || this.GetActualModel() != null)
            {
                this.ThrowNewResponseModelAssertionException();
            }
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
                this.TestContext.MethodResult,
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
                this.TestContext.MethodResult,
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
                this.TestContext.MethodResult,
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
        
        private ObjectResult GetObjectResult()
        {
            var objectResult = this.TestContext.MethodResult as ObjectResult;
            if (objectResult == null)
            {
                throw new InvocationResultAssertionException(string.Format(
                    "{0} action result to inherit from ObjectResult, but it did not.",
                    this.TestContext.ExceptionMessagePrefix));
            }

            return objectResult;
        }

        private void ThrowNewResponseModelAssertionException()
        {
            throw new ResponseModelAssertionException(string.Format(
                "{0} to not have a response model but in fact such was found.",
                this.TestContext.ExceptionMessagePrefix));
        }
    }
}
