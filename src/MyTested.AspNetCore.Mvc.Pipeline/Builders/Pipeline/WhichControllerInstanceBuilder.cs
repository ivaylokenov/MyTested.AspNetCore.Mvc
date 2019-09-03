namespace MyTested.AspNetCore.Mvc.Builders.Pipeline
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Builders.Actions;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Builders.Contracts.CaughtExceptions;
    using Builders.Contracts.Pipeline;
    using Builders.Controllers;
    using Internal.Results;
    using Internal.TestContexts;
    using Utilities;

    public class WhichControllerInstanceBuilder<TController>
        : BaseControllerBuilder<TController, IAndWhichControllerInstanceBuilder<TController>>,
        IAndWhichControllerInstanceBuilder<TController>
        where TController : class
    {
        public WhichControllerInstanceBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<MethodResult> ShouldHave()
            => this
                .InvokeAndGetActionResultTestBuilder()
                .ShouldHave();

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
            => this
                .InvokeAndGetActionResultTestBuilder()
                .ShouldThrow();

        /// <inheritdoc />
        public IShouldReturnTestBuilder<MethodResult> ShouldReturn()
        {
            this.InvokeAction();
            // epa iznesi tuka IActionResultInterface che da e po-lesno iznasqneto tuk + iznesi dolu Invoke na nshto kato ActionCallExpression.Invoke()
            var actionResultTestBuilder = new ActionResultTestBuilder<MethodResult>(this.TestContext);

            return actionResultTestBuilder.ShouldReturn();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction ShouldReturnEmpty()
        {
            this.InvokeAction();

            var actionResultTestBuilder = new VoidActionResultTestBuilder(this.TestContext);

            return actionResultTestBuilder.ShouldReturnEmpty();
        }

        /// <inheritdoc />
        public IWhichControllerInstanceBuilder<TController> AndAlso() => this;

        protected override IAndWhichControllerInstanceBuilder<TController> SetBuilder() => this;

        private IBaseActionResultTestBuilder<MethodResult> InvokeAndGetActionResultTestBuilder()
        {
            this.InvokeAction();

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        private void InvokeAction()
        {
            var methodCall = this.TestContext.MethodCall;
            var methodInfo = ExpressionParser.GetMethodInfo(methodCall);

            if (methodInfo.ReturnType == typeof(void))
            {
                var actionCall = Expression.Lambda<Action<TController>>(
                    methodCall.Body,
                    methodCall.Parameters);

                this.Invoke(actionCall);
            }
            else if (methodInfo.ReturnType == typeof(Task))
            {
                var actionCall = Expression.Lambda<Func<TController, Task>>(
                    methodCall.Body,
                    methodCall.Parameters);

                this.Invoke(actionCall);
            }
            else
            {
                var methodName = nameof(this.CallInvoke);
                var returnType = methodInfo.ReturnType;

                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    methodName = nameof(this.CallTaskInvoke);
                    returnType = returnType.GetGenericArguments().First();
                }

                var invokeActionOpenGeneric = this.GetType().GetMethod(
                    methodName,
                    BindingFlags.Instance | BindingFlags.NonPublic);

                var invokeActionClosedGeneric = invokeActionOpenGeneric.MakeGenericMethod(returnType);

                invokeActionClosedGeneric.Invoke(this, new[] { methodCall });
            }
        }

        // Called via reflection.
        private void CallInvoke<TActionResult>(LambdaExpression methodCall)
        {
            var actionCall = Expression.Lambda<Func<TController, TActionResult>>(
                methodCall.Body,
                methodCall.Parameters);

            this.Invoke(actionCall);
        }

        // Called via reflection.
        private void CallTaskInvoke<TActionResult>(LambdaExpression methodCall)
        {
            var actionCall = Expression.Lambda<Func<TController, Task<TActionResult>>>(
                methodCall.Body,
                methodCall.Parameters);

            this.Invoke(actionCall);
        }
    }
}
