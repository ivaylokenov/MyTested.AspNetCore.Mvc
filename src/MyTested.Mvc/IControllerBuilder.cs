namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Base;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Data;
    using Builders.Contracts.Http;
    using Builders.Contracts.ShouldPassFor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IControllerBuilder<TController> : IBaseTestBuilder
        where TController : class
    {
        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        IControllerTestBuilder ShouldHave();
        
        IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext);

        IAndControllerBuilder<TController> WithControllerContext(Action<ControllerContext> controllerContextSetup);
        
        IAndControllerBuilder<TController> WithActionContext(ActionContext actionContext);

        IAndControllerBuilder<TController> WithActionContext(Action<ActionContext> actionContextSetup);

        /// <summary>
        /// Sets the HTTP context for the current test case.
        /// </summary>
        /// <param name="httpContext">Instance of HttpContext.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext);

        IAndControllerBuilder<TController> WithHttpContext(Action<HttpContext> httpContextSetup);

        /// <summary>
        /// Adds HTTP request message to the tested controller.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpRequest(HttpRequest requestMessage);

        /// <summary>
        /// Adds HTTP request to the tested controller by using builder.
        /// </summary>
        /// <param name="httpRequestBuilder">HTTP request builder.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpRequest(Action<IHttpRequestBuilder> httpRequestBuilder);
        
        IAndControllerBuilder<TController> WithResolvedRouteData();

        IAndControllerBuilder<TController> WithTempData(Action<ITempDataBuilder> tempDataBuilder);

        IAndControllerBuilder<TController> WithMemoryCache(Action<IMemoryCacheBuilder> memoryCacheBuilder);

        IAndControllerBuilder<TController> WithSession(Action<ISessionBuilder> sessionBuilder);

        IAndControllerBuilder<TController> WithNoResolvedDependencyFor<TDependency>()
            where TDependency : class;

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency)
            where TDependency : class;

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided collection of dependencies.
        /// </summary>
        /// <param name="dependencies">Collection of dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencies(IEnumerable<object> dependencies);

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided dependencies.
        /// </summary>
        /// <param name="dependencies">Dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencies(params object[] dependencies);

        /// <summary>
        /// Disables ModelState validation for the action call.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithoutValidation();

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser();

        /// <summary>
        /// Sets custom authenticated user using the provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IClaimsPrincipalBuilder> userBuilder);

        /// <summary>
        /// Sets custom properties to the controller using action delegate.
        /// </summary>
        /// <param name="controllerSetup">Action delegate to use for controller setup.</param>
        /// <returns>The same controller test builder.</returns>
        IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall);

        new IShouldPassForTestBuilderWithController<TController> ShouldPassFor();
    }
}