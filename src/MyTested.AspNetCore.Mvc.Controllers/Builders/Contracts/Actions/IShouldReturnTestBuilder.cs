namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
    using ActionResults.Accepted;
    using ActionResults.Authentication;
    using ActionResults.BadRequest;
    using ActionResults.Conflict;
    using ActionResults.Content;
    using ActionResults.Created;
    using ActionResults.File;
    using ActionResults.LocalRedirect;
    using ActionResults.NotFound;
    using ActionResults.Object;
    using ActionResults.Ok;
    using ActionResults.Redirect;
    using ActionResults.StatusCode;
    using ActionResults.Unauthorized;
    using ActionResults.UnprocessableEntity;
    using And;
    using Invocations;

    /// <summary>
    /// Used for testing returned action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> 
        : IBaseShouldReturnTestBuilder<TActionResult, IAndTestBuilder>
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Accepted();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>.
        /// </summary>
        /// <param name="acceptedTestBuilder">Builder for testing the accepted result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Accepted(Action<IAcceptedTestBuilder> acceptedTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Challenge();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
        /// </summary>
        /// <param name="challengeTestBuilder">Builder for testing the challenge result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Challenge(Action<IChallengeTestBuilder> challengeTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ConflictResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ConflictObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Conflict();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ConflictResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ConflictObjectResult"/>.
        /// </summary>
        /// <param name="conflictTestBuilder">Builder for testing the conflict result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Conflict(Action<IConflictTestBuilder> conflictTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
        /// </summary>
        /// <param name="contentTestBuilder">Builder for testing the content result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Content(Action<IContentTestBuilder> contentTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Created();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
        /// </summary>
        /// <param name="createdTestBuilder">Builder for testing the created result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Created(Action<ICreatedTestBuilder> createdTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder File();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>.
        /// </summary>
        /// <param name="fileTestBuilder">Builder for testing the file result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder File(Action<IFileTestBuilder> fileTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Forbid();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
        /// </summary>
        /// <param name="forbidTestBuilder">Builder for testing the forbid result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Forbid(Action<IForbidTestBuilder> forbidTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Object();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <param name="objectTestBuilder">Builder for testing the object result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Object(Action<IObjectTestBuilder> objectTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.OkResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Ok();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.OkResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/>.
        /// </summary>
        /// <param name="okTestBuilder">Builder for testing the OK result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Ok(Action<IOkTestBuilder> okTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder BadRequest();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
        /// </summary>
        /// <param name="badRequestTestBuilder">Builder for testing the bad request result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder BadRequest(Action<IBadRequestTestBuilder> badRequestTestBuilder);
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder NotFound();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
        /// </summary>
        /// <param name="notFoundTestBuilder">Builder for testing the not found result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder NotFound(Action<INotFoundTestBuilder> notFoundTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Unauthorized();

        /// <summary>	
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>	
        /// or <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult"/>.	
        /// </summary>	
        /// <param name="unauthorizedTestBuilder">Builder for testing the unauthorized result.</param>	
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>	
        IAndTestBuilder Unauthorized(Action<IUnauthorizedTestBuilder> unauthorizedTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder LocalRedirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>.
        /// </summary>
        /// <param name="localRedirectTestBuilder">Builder for testing the local redirect result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder LocalRedirect(Action<ILocalRedirectTestBuilder> localRedirectTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NoContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder NoContent();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder PhysicalFile();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
        /// </summary>
        /// <param name="physicalFileTestBuilder">Builder for testing the physical file result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder PhysicalFile(Action<IPhysicalFileTestBuilder> physicalFileTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Redirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
        /// </summary>
        /// <param name="redirectTestBuilder">Builder for testing the redirect result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Redirect(Action<IRedirectTestBuilder> redirectTestBuilder);
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder SignIn();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>.
        /// </summary>
        /// <param name="signInTestBuilder">Builder for testing the sign in result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder SignIn(Action<ISignInTestBuilder> signInTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder SignOut();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/>.
        /// </summary>
        /// <param name="signOutTestBuilder">Builder for testing the sign out result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder SignOut(Action<ISignOutTestBuilder> signOutTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder StatusCode();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <param name="statusCodeTestBuilder">Builder for testing the status code result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder StatusCode(Action<IStatusCodeTestBuilder> statusCodeTestBuilder);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder UnprocessableEntity();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityObjectResult"/>.
        /// </summary>
        /// <param name="unprocessableEntityTestBuilder">Builder for testing the unprocessable entity result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder UnprocessableEntity(Action<IUnprocessableEntityTestBuilder> unprocessableEntityTestBuilder);

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
