namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpBadRequest
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Models;

    /// <summary>
    /// Used for testing HTTP bad request results.
    /// </summary>
    public interface IHttpBadRequestTestBuilder : IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether HTTP bad request result contains deeply equal error value as the provided error object.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <param name="error">Error object.</param>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TError> WithError<TError>(TError error);

        /// <summary>
        /// Tests whether HTTP bad request result contains error object of the provided type.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TError> WithErrorOfType<TError>();

        /// <summary>
        /// Tests whether no specific error is returned from the HTTP bad request result.
        /// </summary>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithNoError();

        /// <summary>
        /// Tests HTTP bad request result with specific text error message using test builder.
        /// </summary>
        /// <returns>Bad request with error message test builder.</returns>
        IHttpBadRequestErrorMessageTestBuilder WithErrorMessage();

        /// <summary>
        /// Tests HTTP bad request result with specific text error message provided by string.
        /// </summary>
        /// <param name="message">Expected error message from bad request result.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithErrorMessage(string message);

        /// <summary>
        /// Tests whether HTTP bad request result contains the controller's ModelState dictionary as object error.
        /// </summary>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithModelStateError();

        /// <summary>
        /// Tests HTTP bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithModelStateError(ModelStateDictionary modelState);

        /// <summary>
        /// Tests HTTP bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>The same HTTP bad request test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> WithModelStateErrorFor<TRequestModel>();
        
        /// <summary>
        /// Tests whether HTTP bad request result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether HTTP bad request has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether HTTP bad request result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether HTTP bad request result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether HTTP bad request result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether HTTP bad request result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether HTTP bad request result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether HTTP bad request result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether HTTP bad request result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether HTTP bad request result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether HTTP bad request result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether HTTP bad request result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
