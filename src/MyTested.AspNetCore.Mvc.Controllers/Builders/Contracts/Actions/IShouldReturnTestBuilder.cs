namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
    using System.Net;
    using ActionResults.BadRequest;
    using ActionResults.Challenge;
    using ActionResults.Content;
    using ActionResults.Created;
    using ActionResults.File;
    using ActionResults.Forbid;
    using ActionResults.LocalRedirect;
    using ActionResults.NotFound;
    using ActionResults.Object;
    using ActionResults.Ok;
    using ActionResults.Redirect;
    using ActionResults.StatusCode;
    using And;
    using Base;
    using Invocations;

    /// <summary>
    /// Used for testing returned action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> 
        : IBaseShouldReturnTestBuilder<TActionResult, IAndTestBuilder>
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndChallengeTestBuilder"/> type.</returns>
        IAndChallengeTestBuilder Challenge();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        IAndContentTestBuilder Content();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        IAndContentTestBuilder Content(string content);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the content.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        IAndContentTestBuilder Content(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        IAndContentTestBuilder Content(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndCreatedTestBuilder"/> type.</returns>
        IAndCreatedTestBuilder Created();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> or <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndFileTestBuilder"/> type.</returns>
        IAndFileTestBuilder File();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndForbidTestBuilder"/> type.</returns>
        IAndForbidTestBuilder Forbid();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndObjectTestBuilder"/> type.</returns>
        IAndObjectTestBuilder Object();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndOkTestBuilder"/> type.</returns>
        IAndOkTestBuilder Ok();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/> or <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndBadRequestTestBuilder"/> type.</returns>
        IAndBadRequestTestBuilder BadRequest();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndNotFoundTestBuilder"/> type.</returns>
        IAndNotFoundTestBuilder NotFound();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Unauthorized();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndLocalRedirectTestBuilder"/> type.</returns>
        IAndLocalRedirectTestBuilder LocalRedirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NoContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder NoContent();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndPhysicalFileTestBuilder"/> type.</returns>
        IAndPhysicalFileTestBuilder PhysicalFile();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndRedirectTestBuilder"/> type.</returns>
        IAndRedirectTestBuilder Redirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/> or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndStatusCodeTestBuilder"/> type.</returns>
        IAndStatusCodeTestBuilder StatusCode();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/> or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> and it has the same status code as provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>Test builder of <see cref="IAndStatusCodeTestBuilder"/> type.</returns>
        IAndStatusCodeTestBuilder StatusCode(int statusCode);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/> or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> and is the same as provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>Test builder of <see cref="IAndStatusCodeTestBuilder"/> type.</returns>
        IAndStatusCodeTestBuilder StatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.EmptyResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Empty();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnsupportedMediaTypeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder UnsupportedMediaType();
    }
}
