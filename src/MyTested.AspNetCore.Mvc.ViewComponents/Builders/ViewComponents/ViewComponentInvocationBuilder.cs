namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Contracts.Invocations;
    using Invocations;

    public partial class ViewComponentBuilder<TViewComponent>
    {
        /// <inheritdoc />
        public IViewComponentResultTestBuilder<TActionResult> InvokedWith<TActionResult>(Expression<Func<TViewComponent, TActionResult>> actionCall)
        {
            this.Invoke(actionCall);
            return new ViewComponentResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentResultTestBuilder<TActionResult> InvokedWith<TActionResult>(Expression<Func<TViewComponent, Task<TActionResult>>> actionCall)
        {
            this.Invoke(actionCall);
            return new ViewComponentResultTestBuilder<TActionResult>(this.TestContext);
        }

        protected override void ProcessAndValidateMethod(LambdaExpression methodCall, MethodInfo methodInfo)
        {
        }
    }
}
