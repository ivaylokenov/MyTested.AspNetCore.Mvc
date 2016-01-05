namespace MyTested.Mvc
{
    using Microsoft.AspNet.Mvc;
    using System;
    using System.Linq.Expressions;
    using Microsoft.AspNet.Http;
    using System.Threading.Tasks;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Actions;
    using System.Collections.Generic;
    using Builders.Contracts.Authentication;

    /// <summary>
    /// Used for building the action which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
    public interface IControllerBuilder<TController>
        where TController : Controller
    {
        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        IControllerTestBuilder ShouldHave();

        // TODO: with HttpContext?
        ///// <summary>
        ///// Sets the HTTP configuration for the current test case.
        ///// </summary>
        ///// <param name="config">Instance of HttpConfiguration.</param>
        ///// <returns>The same controller builder.</returns>
        //IAndControllerBuilder<TController> WithHttpConfiguration(HttpConfiguration config);

        /// <summary>
        /// Adds HTTP request message to the tested controller.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpRequest(HttpRequest requestMessage);

        ///// <summary>
        ///// Adds HTTP request message to the tested controller.
        ///// </summary>
        ///// <param name="httpRequestBuilder">Builder for HTTP request message.</param>
        ///// <returns>The same controller builder.</returns>
        //IAndControllerBuilder<TController> WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestBuilder);

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
        /// Sets default authenticated user to the built controller with "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser();

        /// <summary>
        /// Sets custom authenticated user using provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IAndUserBuilder> userBuilder);

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
        /// Gets ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <returns>Instance of the ASP.NET Web API controller.</returns>
        TController AndProvideTheController();

        // TODO: ?
        ///// <summary>
        ///// Gets the HTTP configuration used in the testing.
        ///// </summary>
        ///// <returns>Instance of HttpConfiguration.</returns>
        //HttpConfiguration AndProvideTheHttpConfiguration();

        /// <summary>
        /// Gets the HTTP request message used in the testing.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        HttpRequest AndProvideTheHttpRequestMessage();
    }
}