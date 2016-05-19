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
    /// Used for testing returned action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Tests whether the action result is the default value of the type.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> DefaultValue();

        /// <summary>
        /// Tests whether the action result is null.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Null();

        /// <summary>
        /// Tests whether the action result is not null.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotNull();

        /// <summary>
        /// Tests whether the action result is of the provided type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TActionResult}"/> type.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>();

        /// <summary>
        /// Tests whether the action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TActionResult}"/> type.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IChallengeTestBuilder"/> type.</returns>
        IChallengeTestBuilder Challenge();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IContentTestBuilder"/> type.</returns>
        IContentTestBuilder Content();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> with expected content.
        /// </summary>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IContentTestBuilder"/> type.</returns>
        IContentTestBuilder Content(string content);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the content.</param>
        /// <returns>Test builder of <see cref="IContentTestBuilder"/> type.</returns>
        IContentTestBuilder Content(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Test builder of <see cref="IContentTestBuilder"/> type.</returns>
        IContentTestBuilder Content(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="ICreatedTestBuilder"/> type.</returns>
        ICreatedTestBuilder Created();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> or <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IFileTestBuilder"/> type.</returns>
        IFileTestBuilder File();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IForbidTestBuilder"/> type.</returns>
        IForbidTestBuilder Forbid();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IObjectTestBuilder"/> type.</returns>
        IObjectTestBuilder Object();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IOkTestBuilder"/> type.</returns>
        IOkTestBuilder Ok();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/> or <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBadRequestTestBuilder"/> type.</returns>
        IBadRequestTestBuilder BadRequest();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="INotFoundTestBuilder"/> type.</returns>
        INotFoundTestBuilder NotFound();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Unauthorized();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="ILocalRedirectTestBuilder"/> type.</returns>
        ILocalRedirectTestBuilder LocalRedirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.NoContentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NoContent();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> with the default view name.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder PartialView();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> with the provided partial view name.
        /// </summary>
        /// <param name="viewName">Expected partial view name.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder PartialView(string viewName);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder PartialView<TModel>(TModel model);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> with the provided partial view name and deeply equal model object.
        /// </summary>
        /// <param name="viewName">Expected partial view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder PartialView<TModel>(string viewName, TModel model);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IPhysicalFileTestBuilder"/> type.</returns>
        IPhysicalFileTestBuilder PhysicalFile();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IRedirectTestBuilder"/> type.</returns>
        IRedirectTestBuilder Redirect();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> with the default view name.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> with the provided view name.
        /// </summary>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View(string viewName);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> with the provided deeply equal model object.
        /// </summary>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View<TModel>(TModel model);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> with the provided view name and deeply equal model object.
        /// </summary>
        /// <param name="viewName">Expected view name.</param>
        /// <param name="model">Expected model object.</param>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/> type.</returns>
        IViewTestBuilder View<TModel>(string viewName, TModel model);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ViewComponent();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> with the provided view component name.
        /// </summary>
        /// <param name="viewComponentName">Expected view component name.</param>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ViewComponent(string viewComponentName);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> with the provided view component type.
        /// </summary>
        /// <param name="viewComponentType">Expected view component type.</param>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ViewComponent(Type viewComponentType);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> with the specified view component type.
        /// </summary>
        /// <typeparam name="TViewComponentType">Expected view component type.</typeparam>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ViewComponent<TViewComponentType>();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/> and it has the same status code as provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(int statusCode);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/> and is the same as provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.EmptyResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Empty();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.UnsupportedMediaTypeResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> UnsupportedMediaType();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IJsonTestBuilder"/> type.</returns>
        IJsonTestBuilder Json();
    }
}
