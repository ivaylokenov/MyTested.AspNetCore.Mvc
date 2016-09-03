namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Contracts.Invocations;
    using Invocations;
    using Internal.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;

    public partial class ViewComponentBuilder<TViewComponent>
    {
        /// <inheritdoc />
        public IViewComponentResultTestBuilder<TInvocationResult> InvokedWith<TInvocationResult>(Expression<Func<TViewComponent, TInvocationResult>> invocationCall)
        {
            this.Invoke(invocationCall);
            return new ViewComponentResultTestBuilder<TInvocationResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentResultTestBuilder<TInvocationResult> InvokedWith<TInvocationResult>(Expression<Func<TViewComponent, Task<TInvocationResult>>> invocationCall)
        {
            this.Invoke(invocationCall);
            return new ViewComponentResultTestBuilder<TInvocationResult>(this.TestContext);
        }

        protected override void ProcessAndValidateMethod(LambdaExpression invocationCall, MethodInfo methodInfo)
        {
            this.SetViewComponentDescriptor(methodInfo);
            this.ExtractArguments(invocationCall);
        }

        private void SetViewComponentDescriptor(MethodInfo methodInfo)
        {
            var viewComponentContext = this.TestContext.ViewComponentContext;
            if (viewComponentContext.ViewComponentDescriptor?.MethodInfo == null)
            {
                var viewComponentDescriptorCache = this.Services.GetService<IViewComponentDescriptorCache>();
                if (viewComponentDescriptorCache == null)
                {
                    viewComponentContext.ViewComponentDescriptor
                        = viewComponentDescriptorCache.TryGetViewComponentDescriptor(methodInfo);
                }
            }
        }

        private void ExtractArguments(LambdaExpression invocationCall)
        {
            ExpressionParser
                .ResolveMethodArguments(invocationCall)
                .ForEach(arg => this.TestContext.ViewComponentContext.Arguments.Add(arg.Name, arg.Value));
        }
    }
}
