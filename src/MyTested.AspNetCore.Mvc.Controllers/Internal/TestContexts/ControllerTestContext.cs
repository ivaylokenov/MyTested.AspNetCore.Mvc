namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Linq.Expressions;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    public class ControllerTestContext : ActionTestContext<ControllerContext>
    {
        public override string ExceptionMessagePrefix => $"When calling {this.MethodName} action in {this.Component.GetName()} expected";
        
        public override LambdaExpression RouteDataMethodCall => this.MethodCall;

        protected override ControllerContext DefaultComponentContext
            => ControllerContextMock.Default(this);

        protected override ControllerContext ExecutionComponentContext
            => this.HttpContext.Features.Get<ExecutionTestContext>()?.ControllerContext;
    }
}
