namespace MyTested.AspNetCore.Mvc.Builders.Components
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Internal.Results;
    using Internal.TestContexts;
    using Utilities;
    using System.Reflection;

    public partial class BaseComponentBuilder<TComponent, TTestContext, TBuilder>
    {
        protected void Invoke<TMethodResult>(Expression<Func<TComponent, TMethodResult>> methodCall)
        {
            var actionInfo = this.GetAndValidateMethodResult(methodCall);
            this.TestContext.Apply(actionInfo);
        }

        protected void Invoke<TMethodResult>(Expression<Func<TComponent, Task<TMethodResult>>> methodCall)
        {
            var methodInfo = this.GetAndValidateMethodResult(methodCall);
            var methodResult = default(TMethodResult);

            try
            {
                methodResult = AsyncHelper.RunSync(() => methodInfo.MethodResult);
            }
            catch (Exception exception)
            {
                methodInfo.CaughtException = new AggregateException(exception);
            }

            this.TestContext.Apply(methodInfo);
            this.TestContext.MethodResult = methodResult;
        }

        protected void Invoke(Expression<Action<TComponent>> methodCall)
        {
            var methodName = this.GetAndValidateMethod(methodCall);
            Exception caughtException = null;

            try
            {
                methodCall.Compile().Invoke(this.Component);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            this.TestContext.MethodName = methodName;
            this.TestContext.MethodCall = methodCall;
            this.TestContext.CaughtException = caughtException;
            this.TestContext.MethodResult = VoidMethodResult.Instance;
        }

        protected void Invoke(Expression<Func<TComponent, Task>> methodCall)
        {
            var methodInfo = this.GetAndValidateMethodResult(methodCall);

            try
            {
                AsyncHelper.RunSync(() => methodInfo.MethodResult);
            }
            catch (Exception exception)
            {
                methodInfo.CaughtException = new AggregateException(exception);
            }

            this.TestContext.Apply(methodInfo);
            this.TestContext.MethodResult = VoidMethodResult.Instance;
        }

        protected abstract void ProcessAndValidateMethod(LambdaExpression methodCall, MethodInfo methodInfo);

        private InvocationTestContext<TMethodResult> GetAndValidateMethodResult<TMethodResult>(Expression<Func<TComponent, TMethodResult>> methodCall)
        {
            var methodName = this.GetAndValidateMethod(methodCall);
            var methodResult = default(TMethodResult);
            Exception caughtException = null;

            try
            {
                methodResult = methodCall.Compile().Invoke(this.Component);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return new InvocationTestContext<TMethodResult>(methodName, methodCall, methodResult, caughtException);
        }

        private string GetAndValidateMethod(LambdaExpression methodCall)
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();

            this.TestContext.MethodCall = methodCall;
            this.TestContext.PreMethodInvocationDelegate?.Invoke();

            var methodInfo = ExpressionParser.GetMethodInfo(methodCall);

            this.ProcessAndValidateMethod(methodCall, methodInfo);

            return methodInfo.Name;
        }
    }
}
