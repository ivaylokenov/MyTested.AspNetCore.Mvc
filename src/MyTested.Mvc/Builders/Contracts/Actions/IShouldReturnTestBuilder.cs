namespace MyTested.Mvc.Builders.Contracts.Actions
{
    using System;
    using System.Net;
    using ActionResults.BadRequest;
    using ActionResults.Challenge;
    using ActionResults.Content;
    using ActionResults.Created;
    using ActionResults.File;
    using ActionResults.Forbid;
    using ActionResults.Json;
    using ActionResults.LocalRedirect;
    using ActionResults.NotFound;
    using ActionResults.Object;
    using ActionResults.Ok;
    using ActionResults.Redirect;
    using ActionResults.View;
    using Base;
    using Models;

    /// <summary>
    /// Used for testing action returned result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
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

        /// <summary>
        /// Tests whether action result is ChallengeResult.
        /// </summary>
        /// <returns>Challenge test builder.</returns>
        IChallengeTestBuilder Challenge();

        /// <summary>
        /// Tests whether action result is ContentResult.
        /// </summary>
        /// <returns>Content test builder.</returns>
        IContentTestBuilder Content();

        /// <summary>
        /// Tests whether action result is ContentResult with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Content test builder.</returns>
        IContentTestBuilder Content(string content);

        /// <summary>
        /// Tests whether content result passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the content.</param>
        /// <returns>Content result test builder.</returns>
        IContentTestBuilder Content(Action<string> assertions);

        /// <summary>
        /// Tests whether content result passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Content result test builder.</returns>
        IContentTestBuilder Content(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether action result is CreatedResult, CreatedAtActionResult or CreatedAtRouteResult.
        /// </summary>
        /// <returns>Created test builder.</returns>
        ICreatedTestBuilder Created();

        /// <summary>
        /// Tests whether action result is FileStreamResult, VirtualFileResult or FileContentResult.
        /// </summary>
        /// <returns>File test builder.</returns>
        IFileTestBuilder File();

        /// <summary>
        /// Tests whether action result is ForbidResult.
        /// </summary>
        /// <returns>Forbid test builder.</returns>
        IForbidTestBuilder Forbid();

        IObjectTestBuilder Object();

        /// <summary>
        /// Tests whether action result is OkResult or OkObjectResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        IOkTestBuilder Ok();
        
        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        IBadRequestTestBuilder BadRequest();

        /// <summary>
        /// Tests whether action result is NotFoundResult or NotFoundObjectResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        INotFoundTestBuilder NotFound();
        
        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Unauthorized();
        
        /// <summary>
        /// Tests whether action result is LocalRedirectResult.
        /// </summary>
        /// <returns>Local redirect test builder.</returns>
        ILocalRedirectTestBuilder LocalRedirect();

        /// <summary>
        /// Tests whether action result is NoContentResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NoContent();

        /// <summary>
        /// Tests whether action result is PartialViewResult with default view name.
        /// </summary>
        /// <returns>View test builder.</returns>
        IViewTestBuilder PartialView();
        
        /// <summary>
        /// Tests whether action result is PartialViewResult with the specified view name.
        /// </summary>
        /// <param name="viewName">Expected partial view name.</param>
        /// <returns>View test builder.</returns>
        IViewTestBuilder PartialView(string viewName);

        /// <summary>
        /// Tests whether action result is PhysicalFileResult.
        /// </summary>
        /// <returns>File test builder.</returns>
        IPhysicalFileTestBuilder PhysicalFile();

        /// <summary>
        /// Tests whether action result is RedirectResult, RedirectToActionResult or RedirectToRouteResult.
        /// </summary>
        /// <returns>Redirect test builder.</returns>
        IRedirectTestBuilder Redirect();

        /// <summary>
        /// Tests whether action result is ViewResult with default view name.
        /// </summary>
        /// <returns>View test builder.</returns>
        IViewTestBuilder View();

        /// <summary>
        /// Tests whether action result is ViewResult with the specified view name.
        /// </summary>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>View test builder.</returns>
        IViewTestBuilder View(string viewName);
        
        /// <summary>
        /// Tests whether action result is ViewComponentResult.
        /// </summary>
        /// <returns>View component test builder.</returns>
        IViewComponentTestBuilder ViewComponent();
        
        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component name.
        /// </summary>
        /// <param name="viewComponentName">Expected view component name.</param>
        /// <returns>View component test builder.</returns>
        IViewComponentTestBuilder ViewComponent(string viewComponentName);
        
        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component type.
        /// </summary>
        /// <param name="viewComponentType">Expected view component type.</param>
        /// <returns>View component test builder.</returns>
        IViewComponentTestBuilder ViewComponent(Type viewComponentType);

        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component type.
        /// </summary>
        /// <typeparam name="TViewComponentType">Expected view component type.</typeparam>
        /// <returns>View component test builder.</returns>
        IViewComponentTestBuilder ViewComponent<TViewComponentType>();

        /// <summary>
        /// Tests whether action result is StatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode();

        /// <summary>
        /// Tests whether action result is StatusCodeResult and is the same as provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(int statusCode);

        /// <summary>
        /// Tests whether action result is StatusCodeResult and is the same as provided StatusCode.
        /// </summary>
        /// <param name="statusCode">StatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode);
        
        /// <summary>
        /// Tests whether action result is EmptyResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Empty();

        /// <summary>
        /// Tests whether action result is UnsupportedMediaTypeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> UnsupportedMediaType();
        
        /// <summary>
        /// Tests whether action result is JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        IJsonTestBuilder Json();
    }
}
