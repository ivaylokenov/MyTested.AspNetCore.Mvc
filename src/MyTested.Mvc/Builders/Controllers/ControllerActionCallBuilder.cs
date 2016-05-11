namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Actions;
    using Contracts.Actions;
    using Contracts.Controllers;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithoutValidation()
        {
            this.enabledValidation = false;
            return this;
        }

        /// <inheritdoc />
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            this.TestContext.Apply(actionInfo);

            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
            this.BuildControllerIfNotExists();
            this.SetRouteData(actionCall);

            var methodInfo = ExpressionParser.GetMethodInfo(actionCall);

            if (this.enabledValidation)
            {
                this.ValidateModelState(actionCall);
            }

            this.SetActionDescriptor(methodInfo);
            
            return methodInfo.Name;
        }

        private void ValidateModelState(LambdaExpression actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall);
            if (arguments.Any())
            {
                var validator = this.Services.GetRequiredService<IObjectModelValidator>();
                arguments.ForEach(argument =>
                {
                    if (argument.Value != null)
                    {
                        validator.Validate(this.TestContext.ControllerContext, argument.Value);
                    }
                });
            }
        }

        private void SetActionDescriptor(MethodInfo methodInfo)
        {
            var controllerContext = this.TestContext.ControllerContext;
            if (controllerContext.ActionDescriptor == null || controllerContext.ActionDescriptor.MethodInfo == null)
            {
                var controllerActionDescriptorCache = this.Services.GetService<IControllerActionDescriptorCache>();
                if (controllerActionDescriptorCache != null)
                {
                    controllerContext.ActionDescriptor
                        = controllerActionDescriptorCache.TryGetActionDescriptor(methodInfo);
                }
            }
        }
    }
}
