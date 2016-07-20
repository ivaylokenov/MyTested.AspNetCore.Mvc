namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Actions;
    using Authentication;
    using Base;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ShouldPassFor;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IControllerBuilder<TController> : IBaseTestBuilderWithComponentBuilder<IAndControllerBuilder<TController>>
        where TController : class
    {
        /// <summary>
        /// Used for testing controller additional data.
        /// </summary>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IControllerTestBuilder ShouldHave();

        /// <summary>
        /// Sets the <see cref="ControllerContext"/> on the tested controller.
        /// </summary>
        /// <param name="controllerContext">Instance of <see cref="ControllerContext"/> to set.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext);

        /// <summary>
        /// Sets the <see cref="ControllerContext"/> on the tested controller.
        /// </summary>
        /// <param name="controllerContextSetup">Action setting the <see cref="ControllerContext"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithControllerContext(Action<ControllerContext> controllerContextSetup);

        /// <summary>
        /// Sets the <see cref="ActionContext"/> on the tested controller.
        /// </summary>
        /// <param name="actionContext">Instance of <see cref="ActionContext"/> to set.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithActionContext(ActionContext actionContext);

        /// <summary>
        /// Sets the <see cref="ActionContext"/> on the tested controller.
        /// </summary>
        /// <param name="actionContextSetup">Action setting the <see cref="ActionContext"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithActionContext(Action<ActionContext> actionContextSetup);

        /// <summary>
        /// Sets the <see cref="HttpContext"/> on the tested controller.
        /// </summary>
        /// <param name="httpContext">Instance of <see cref="HttpContext"/> to set.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext);

        /// <summary>
        /// Sets the <see cref="HttpContext"/> on the tested controller.
        /// </summary>
        /// <param name="httpContextSetup">Action setting the <see cref="HttpContext"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithHttpContext(Action<HttpContext> httpContextSetup);

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> on the tested controller.
        /// </summary>
        /// <param name="httpRequest">Instance of <see cref="HttpRequest"/> to set.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithHttpRequest(HttpRequest httpRequest);

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> on the tested controller.
        /// </summary>
        /// <param name="httpRequestBuilder">Action setting the <see cref="HttpRequest"/> by using <see cref="IHttpRequestBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithHttpRequest(Action<IHttpRequestBuilder> httpRequestBuilder);

        /// <summary>
        /// Indicates that route values should be extracted from the provided action call expression.
        /// </summary>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithRouteData();

        /// <summary>
        /// Indicates that route values should be extracted from the provided action call expression adding the given additional values.
        /// </summary>
        /// <param name="additionalRouteValues">Anonymous object containing route values.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithRouteData(object additionalRouteValues);
        
        /// <summary>
        /// Configures a service with scoped lifetime by using the provided <see cref="Action"/> delegate.
        /// </summary>
        /// <typeparam name="TService">Type of service to configure.</typeparam>
        /// <param name="scopedServiceSetup">Action configuring the service before running the test case.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithServiceSetupFor<TService>(Action<TService> scopedServiceSetup)
            where TService : class;
        
        /// <summary>
        /// Sets null value to the constructor service dependency of the given type.
        /// </summary>
        /// <typeparam name="TService">Type of service dependency.</typeparam>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithNoServiceFor<TService>()
            where TService : class;

        /// <summary>
        /// Tries to resolve constructor service dependency of the given type.
        /// </summary>
        /// <typeparam name="TService">Type of service dependency to resolve.</typeparam>
        /// <param name="service">Instance of service dependency to inject into the controller constructor.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithServiceFor<TService>(TService service)
            where TService : class;

        /// <summary>
        /// Tries to resolve constructor service dependencies by the provided collection of objects.
        /// </summary>
        /// <param name="services">Collection of service dependencies to inject into the controller constructor.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithServices(IEnumerable<object> services);

        /// <summary>
        /// Tries to resolve constructor service dependencies by the provided parameters.
        /// </summary>
        /// <param name="services">Services to inject into the controller constructor.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithServices(params object[] services);

        /// <summary>
        /// Disables <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> validation for the action call.
        /// </summary>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithoutValidation();

        /// <summary>
        /// Sets default authenticated <see cref="HttpContext.User"/> to the built controller with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser();

        /// <summary>
        /// Sets custom authenticated <see cref="HttpContext.User"/> to the built controller using the provided user builder.
        /// </summary>
        /// <param name="userBuilder">Action setting the <see cref="HttpContext.User"/> by using <see cref="IClaimsPrincipalBuilder"/>.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IClaimsPrincipalBuilder> userBuilder);

        /// <summary>
        /// Sets custom properties to the controller using a delegate.
        /// </summary>
        /// <param name="controllerSetup">Action to use for controller setup.</param>
        /// <returns>The same <see cref="IControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked asynchronous action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked void action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked asynchronous void action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall);

        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithController{TController}"/> type.</returns>
        new IShouldPassForTestBuilderWithController<TController> ShouldPassFor();
    }
}