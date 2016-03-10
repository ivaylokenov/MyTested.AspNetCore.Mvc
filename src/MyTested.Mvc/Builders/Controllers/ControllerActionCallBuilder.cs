namespace MyTested.Mvc.Builders.Controllers
{
    using Actions;
    using Contracts.Actions;
    using Contracts.Controllers;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Utilities;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public partial class ControllerBuilder<TController>
    {
        /// <summary>
        /// Disables ModelState validation for the action call.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithoutValidation()
        {
            this.enabledValidation = false;
            return this;
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            this.TestContext.Apply(actionInfo);

            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);
            var actionResult = default(TActionResult);

            try
            {
                actionResult = actionInfo.ActionResult.Result;
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            this.TestContext.Apply(actionInfo);
            this.TestContext.ActionResult = actionResult;

            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            Exception caughtException = null;

            try
            {
                actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            this.TestContext.ActionName = actionName;
            this.TestContext.ActionCall = actionCall;
            this.TestContext.CaughtException = caughtException;

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            try
            {
                actionInfo.ActionResult.Wait();
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            this.TestContext.Apply(actionInfo);

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        private ActionTestContext<TActionResult> GetAndValidateActionResult<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            var action = ExpressionParser.GetMethodInfo(actionCall);
            var actionResult = default(TActionResult);
            Exception caughtException = null;

            try
            {
                actionResult = actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return new ActionTestContext<TActionResult>(actionName, actionCall, actionResult, caughtException);
        }

        private string GetAndValidateAction(LambdaExpression actionCall)
        {
            this.SetRouteData(actionCall);

            var methodInfo = ExpressionParser.GetMethodInfo(actionCall);

            if (this.enabledValidation)
            {
                this.ValidateModelState(actionCall);
            }

            var controllerActionDescriptorCache = this.Services.GetService<IControllerActionDescriptorCache>();
            if (controllerActionDescriptorCache != null)
            {
                this.Controller.ControllerContext.ActionDescriptor
                    = controllerActionDescriptorCache.TryGetActionDescriptor(methodInfo);
            }

            return methodInfo.Name;
        }

        private void ValidateModelState(LambdaExpression actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall);
            foreach (var argument in arguments)
            {
                if (argument.Value != null)
                {
                    this.Controller.TryValidateModel(argument.Value);
                }
            }
        }
    }
}
