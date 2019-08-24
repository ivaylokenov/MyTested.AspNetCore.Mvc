namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    using System;
    using Builders.Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
    public interface IBaseControllerBuilder<TController, TBuilder> : IBaseTestBuilderWithComponentBuilder<IAndControllerBuilder<TController>>
        where TController : class
    {
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
    }
}
