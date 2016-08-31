namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Linq;
    using Application;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Routing;
    using Utilities.Validators;

    public abstract class ActionTestContext<TComponentContext> : ComponentTestContext
        where TComponentContext : ActionContext
    {
        private TComponentContext componentContext;
        private RouteData expressionRouteData;

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
                this.ComponentContext.RouteData = value;
                base.RouteData = value;
            }
        }
        
        public TComponentContext ComponentContext
        {
            get
            {
                if (this.componentContext == null)
                {
                    this.componentContext = this.DefaultComponentContext;
                    if (!this.componentContext.RouteData.Values.Any())
                    {
                        this.componentContext.RouteData = this.RouteData ?? this.componentContext.RouteData;
                    }
                }

                return this.componentContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TComponentContext));
                this.componentContext = value;
            }
        }

        public Action<TComponentContext> ComponentContextPreparationDelegate { get; set; }

        protected abstract TComponentContext DefaultComponentContext { get; }
    }
}
