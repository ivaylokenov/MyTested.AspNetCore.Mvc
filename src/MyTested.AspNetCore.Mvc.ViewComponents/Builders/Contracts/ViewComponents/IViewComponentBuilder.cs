namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponents
{
    using System;
    using System.Linq.Expressions;
    using Base;
    using Invocations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using System.Threading.Tasks;

    /// <summary>
    /// Used for building the view component which will be tested.
    /// </summary>
    /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
    public interface IViewComponentBuilder<TViewComponent> : IBaseTestBuilderWithComponentBuilder<IAndViewComponentBuilder<TViewComponent>>
        where TViewComponent : class
    {
        /// <summary>
        /// Used for testing view component additional details.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentTestBuilder"/> type.</returns>
        IViewComponentTestBuilder ShouldHave();

        /// <summary>
        /// Sets the <see cref="ViewComponentContext"/> on the tested view component.
        /// </summary>
        /// <param name="viewComponentContext">Instance of <see cref="ViewComponentContext"/> to set.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithViewComponentContext(
            ViewComponentContext viewComponentContext);

        /// <summary>
        /// Sets the <see cref="ViewComponentContext"/> on the tested controller.
        /// </summary>
        /// <param name="viewComponentContextSetup">Action setting the <see cref="ViewComponentContext"/>.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithViewComponentContext(
            Action<ViewComponentContext> viewComponentContextSetup);

        /// <summary>
        /// Sets the <see cref="ViewContext"/> on the tested view component.
        /// </summary>
        /// <param name="viewContext">Instance of <see cref="ViewContext"/> to set.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithViewContext(
            ViewContext viewContext);

        /// <summary>
        /// Sets the <see cref="ViewContext"/> on the tested controller.
        /// </summary>
        /// <param name="viewContextSetup">Action setting the <see cref="ViewContext"/>.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithViewContext(
           Action<ViewContext> viewContextSetup);

        /// <summary>
        /// Sets the <see cref="ActionContext"/> on the tested view component.
        /// </summary>
        /// <param name="actionContext">Instance of <see cref="ActionContext"/> to set.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithActionContext(
            ActionContext actionContext);

        /// <summary>
        /// Sets the <see cref="ActionContext"/> on the tested controller.
        /// </summary>
        /// <param name="actionContextSetup">Action setting the <see cref="ActionContext"/>.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithActionContext(
            Action<ActionContext> actionContextSetup);

        /// <summary>
        /// Sets custom properties to the view component using a delegate.
        /// </summary>
        /// <param name="viewComponentSetup">Action to use for view component setup.</param>
        /// <returns>The same <see cref="IViewComponentBuilder{TViewComponent}"/>.</returns>
        IAndViewComponentBuilder<TViewComponent> WithSetup(Action<TViewComponent> viewComponentSetup);

        /// <summary>
        /// Indicates which view component method should be invoked and tested.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of result from the invocation.</typeparam>
        /// <param name="invocationCall">Method call expression indicating invoked method.</param>
        /// <returns>Test builder of <see cref="IViewComponentResultTestBuilder{TInvocationResult}"/> type.</returns>
        IViewComponentResultTestBuilder<TInvocationResult> InvokedWith<TInvocationResult>(Expression<Func<TViewComponent, TInvocationResult>> invocationCall);

        /// <summary>
        /// Indicates which view component method should be invoked and tested.
        /// </summary>
        /// <typeparam name="TInvocationResult">Type of result from the invocation.</typeparam>
        /// <param name="invocationCall">Method call expression indicating invoked asynchronous method.</param>
        /// <returns>Test builder of <see cref="IViewComponentResultTestBuilder{TInvocationResult}"/> type.</returns>
        IViewComponentResultTestBuilder<TInvocationResult> InvokedWith<TInvocationResult>(Expression<Func<TViewComponent, Task<TInvocationResult>>> invocationCall);
    }
}
