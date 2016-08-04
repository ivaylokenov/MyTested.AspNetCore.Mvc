namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Actions;
    using Base;
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
        /// <returns>Test builder of <see cref="IControllerTestBuilder"/> type.</returns>
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