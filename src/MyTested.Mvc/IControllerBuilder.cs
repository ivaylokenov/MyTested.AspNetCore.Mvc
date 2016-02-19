namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public interface IControllerBuilder<TController>
        where TController : Controller
    {
        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        IControllerTestBuilder ShouldHave();

        /// <summary>
        /// Sets the HTTP context for the current test case.
        /// </summary>
        /// <param name="httpContext">Instance of HttpContext.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext);

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

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency);

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
        IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IAndClaimsPrincipalBuilder> userBuilder);

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
        IActionResultTestBuilder<TActionResult> CallingAsync<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall);

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
        IVoidActionResultTestBuilder CallingAsync(Expression<Func<TController, Task>> actionCall);

        /// <summary>
        /// Gets ASP.NET MVC controller instance to be tested.
        /// </summary>
        /// <returns>Instance of the ASP.NET MVC controller.</returns>
        TController AndProvideTheController();

        /// <summary>
        /// Gets the HTTP request message with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequest from the tested controller.</returns>
        HttpRequest AndProvideTheHttpRequest();

        /// <summary>
        /// Gets the HTTP context with which the action will be tested.
        /// </summary>
        /// <returns>HttpContext from the tested controller.</returns>
        HttpContext AndProvideTheHttpContext();
    }
}