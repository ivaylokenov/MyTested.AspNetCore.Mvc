namespace MyTested.Mvc.Internal.TestContexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;
    using Routes;
    using Application;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    public class ControllerTestContext : HttpTestContext
    {
        private ControllerContext controllerContext;
        private IEnumerable<object> controllerAttributes;
        private string actionName;
        private LambdaExpression actionCall;
        private IEnumerable<object> actionAttributes;
        private object model;
        private RouteData expressionRouteData;
        
        public object Controller { get; internal set; }

        public IEnumerable<object> ControllerAttributes
        {
            get
            {
                if (this.controllerAttributes == null)
                {
                    this.controllerAttributes = Reflection.GetCustomAttributes(this.Controller);
                }

                return this.controllerAttributes;
            }
        }

        public string ActionName
        {
            get
            {
                return this.actionName;
            }

            internal set
            {
                CommonValidator.CheckForNotWhiteSpaceString(value, nameof(ActionName));
                this.actionName = value;
            }
        }

        public MethodInfo Action
        {
            get
            {
                return ExpressionParser.GetMethodInfo(this.ActionCall);
            }
        }

        public LambdaExpression ActionCall
        {
            get
            {
                return this.actionCall;
            }

            internal set
            {
                CommonValidator.CheckForNullReference(value, nameof(ActionCall));
                this.actionCall = value;
            }
        }

        public object ActionResult { get; internal set; }

        public IEnumerable<object> ActionAttributes
        {
            get
            {
                if (this.actionAttributes == null)
                {
                    this.actionAttributes = Reflection.GetCustomAttributes(this.Action);
                }

                return this.actionAttributes;
            }
        }

        public Exception CaughtException { get; internal set; }

        public object Model
        {
            get
            {
                if (this.model == null)
                {
                    return this.ActionResult;
                }

                return this.model;
            }

            internal set
            {
                this.model = value;
            }
        }

        public override RouteData RouteData
        {
            get
            {
                var routeData = base.RouteData;
                if (routeData != null)
                {
                    return routeData;
                }

                if (this.expressionRouteData == null && this.ActionCall != null)
                {
                    this.expressionRouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, this.ActionCall);
                }

                return this.expressionRouteData;
            }
            
            internal set
            {
                CommonValidator.CheckForNullReference(value, nameof(RouteData));
                this.ControllerContext.RouteData = value;
                base.RouteData = value;
            }
        }

        public ITempDataDictionary TempData => this.ControllerAs<Controller>().TempData;

        public ViewDataDictionary ViewData => this.ControllerAs<Controller>().ViewData;

        internal TController ControllerAs<TController>()
            where TController : class => this.Controller as TController;

        internal ControllerContext ControllerContext
        {
            get
            {
                if (this.controllerContext == null)
                {
                    this.controllerContext = this.ControllerAs<Controller>()?.ControllerContext ?? new MockedControllerContext(this);
                    if (this.controllerContext.RouteData == null || !this.controllerContext.RouteData.Values.Any())
                    {
                        this.controllerContext.RouteData = this.RouteData;
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

        internal TActionResult ActionResultAs<TActionResult>() => this.ActionResult.TryCastTo<TActionResult>();

        internal TException CaughtExceptionAs<TException>()
            where TException : Exception => this.CaughtException as TException;

        internal TModel ModelAs<TModel>() => this.Model.TryCastTo<TModel>();

        internal void Apply<TActionResult>(ActionTestContext<TActionResult> testActionDescriptor)
        {
            this.ActionName = testActionDescriptor.ActionName;
            this.ActionCall = testActionDescriptor.ActionCall;
            this.ActionResult = testActionDescriptor.ActionResult;
            this.CaughtException = testActionDescriptor.CaughtException;
        }
    }
}
