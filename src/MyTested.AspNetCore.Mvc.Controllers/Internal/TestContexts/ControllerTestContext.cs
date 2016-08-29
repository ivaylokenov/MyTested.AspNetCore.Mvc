namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Linq;
    using Application;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;
    using Utilities.Validators;
    using Routing;

    public class ControllerTestContext : ComponentTestContext
    {
        private ControllerContext controllerContext;
        private RouteData expressionRouteData;

        public ModelStateDictionary ModelState => this.ControllerContext.ModelState;

        public override string ExceptionMessagePrefix => $"When calling {this.MethodName} action in {this.Component.GetName()} expected";
        
        public override RouteData RouteData
        {
            get
            {
                var routeData = base.RouteData;
                if (routeData != null)
                {
                    return routeData;
                }

                if (this.expressionRouteData == null && this.MethodCall != null)
                {
                    this.expressionRouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, this.MethodCall);
                }

                return this.expressionRouteData;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(RouteData));
                this.ControllerContext.RouteData = value;
                base.RouteData = value;
            }
        }

        internal ControllerContext ControllerContext
        {
            get
            {
                if (this.controllerContext == null)
                {
                    this.controllerContext = ControllerContextMock.Default(this);
                    if (!this.controllerContext.RouteData.Values.Any())
                    {
                        this.controllerContext.RouteData = this.RouteData ?? this.controllerContext.RouteData;
                    }
                }

                return this.controllerContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(ControllerContext));
                this.controllerContext = value;
            }
        }
        
        internal void Apply<TActionResult>(ActionTestContext<TActionResult> actionTestContext)
        {
            this.MethodName = actionTestContext.ActionName;
            this.MethodCall = actionTestContext.ActionCall;
            this.MethodResult = actionTestContext.ActionResult;
            this.CaughtException = actionTestContext.CaughtException;
        }
    }
}
