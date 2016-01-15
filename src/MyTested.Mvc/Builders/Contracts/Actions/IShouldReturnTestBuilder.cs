namespace MyTested.Mvc.Builders.Contracts.Actions
{
    using System;
    using System.Net;
    using ActionResults.Challenge;
    using ActionResults.Content;
    using ActionResults.Created;
    using ActionResults.File;
    using ActionResults.HttpBadRequest;
    using ActionResults.HttpNotFound;
    using ActionResults.Json;
    using ActionResults.LocalRedirect;
    using ActionResults.Ok;
    using Base;
    using Models;

    /// <summary>
    /// Used for testing action returned result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> DefaultValue();

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Null();

        /// <summary>
        /// Tests whether action result is not null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotNull();
        
        /// <summary>
        /// Tests whether action result is OkResult or OkNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        IOkTestBuilder Ok();

        /// <summary>
        /// Tests whether action result is ChallengeResult.
        /// </summary>
        /// <returns>Challenge test builder.</returns>
        IChallengeTestBuilder Challenge();

        /// <summary>
        /// Tests whether action result is CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Created test builder.</returns>
        ICreatedTestBuilder Created();

        /// <summary>
        /// Tests whether action result is FileStreamResult, VirtualFileResult or FileContentResult.
        /// </summary>
        /// <returns>File test builder.</returns>
        IFileTestBuilder File();

        /// <summary>
        /// Tests whether action result is ContentResult.
        /// </summary>
        /// <returns>Content test builder.</returns>
        IContentTestBuilder Content();

        /// <summary>
        /// Tests whether action result is ContentResult.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Content test builder.</returns>
        IContentTestBuilder Content(string content);

        /// <summary>
        /// Tests whether action result is NoContentResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NoContent();

        /// <summary>
        /// Tests whether action result is LocalRedirectResult.
        /// </summary>
        /// <returns>Local redirect test builder.</returns>
        ILocalRedirectTestBuilder LocalRedirect();
        
        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode();

        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult and is the same as provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(int statusCode);

        /// <summary>
        /// Tests whether action result is HttpStatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether action result is UnsupportedMediaTypeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> UnsupportedMediaType();

        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IHttpNotFoundTestBuilder NotFound();

        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        IHttpBadRequestTestBuilder HttpBadRequest();
        
        /// <summary>
        /// Tests whether action result is HttpUnauthorizedResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> HttpUnauthorized();
        
        /// <summary>
        /// Tests whether action result is JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        IJsonTestBuilder Json();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType);
    }
}
