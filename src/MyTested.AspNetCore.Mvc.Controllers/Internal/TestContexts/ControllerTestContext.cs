namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Linq.Expressions;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Utilities.Extensions;

    public class ControllerTestContext : ActionTestContext<ControllerContext>
    {
        public override string ExceptionMessagePrefix => $"When calling {this.MethodName} action in {this.Component.GetName()} expected";
        
        public override LambdaExpression RouteDataMethodCall => this.MethodCall;

        protected override ControllerContext DefaultComponentContext
            => ControllerContextMock.Default(this);

        protected override ControllerContext ExecutionComponentContext
            => this.HttpContext.Features.Get<ExecutionTestContext>()?.ControllerContext;

        protected override object ConvertMethodResult(object methodResult)
        {
            if (methodResult is IConvertToActionResult converter)
            {
                methodResult = converter.Convert();

                if (methodResult is ObjectResult objectResult)
                {
                    methodResult = objectResult.Value;
                }
            }

            return methodResult;
        }
    }
}
