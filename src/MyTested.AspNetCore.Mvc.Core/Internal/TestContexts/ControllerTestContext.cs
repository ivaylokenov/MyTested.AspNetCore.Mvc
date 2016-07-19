namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Application;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Routing;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class ControllerTestContext : HttpTestContext
    {
        private object controller;
        private ControllerContext controllerContext;
        private IEnumerable<object> controllerAttributes;
        private string actionName;
        private MethodInfo action;
        private LambdaExpression actionCall;
        private IEnumerable<object> actionAttributes;
        private object model;
        private RouteData expressionRouteData;

        public object Controller
        {
            get
            {
                if (this.controller == null)
                {
                    this.controller = this.ControllerConstruction();
                }

                return this.controller;
            }
        }

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
                CommonValidator.CheckForNotWhiteSpaceString(value, nameof(this.ActionName));
                this.actionName = value;
            }
        }

        public MethodInfo Action
        {
            get
            {
                if (this.action == null)
                {
                    this.action = ExpressionParser.GetMethodInfo(this.ActionCall);
                }

                return this.action;
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
                CommonValidator.CheckForNullReference(value, nameof(this.ActionCall));
                this.actionCall = value;
            }
        }

        public object ActionResult { get; set; }

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

            set
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

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(RouteData));
                this.ControllerContext.RouteData = value;
                base.RouteData = value;
            }
        }

        public ModelStateDictionary ModelState => this.ControllerContext.ModelState;

        public override string ExceptionMessagePrefix => $"When calling {this.ActionName} action in {this.Controller.GetName()} expected";

        internal Func<object> ControllerConstruction { get; set; }

        internal ControllerContext ControllerContext
        {
            get
            {
                if (this.controllerContext == null)
                {
                    this.controllerContext = new MockedControllerContext(this);
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

        internal TController ControllerAs<TController>()
            where TController : class => this.Controller as TController;

        internal TActionResult ActionResultAs<TActionResult>() => this.ActionResult.TryCastTo<TActionResult>();

        internal TException CaughtExceptionAs<TException>()
            where TException : Exception => this.CaughtException as TException;

        internal TModel ModelAs<TModel>() => this.Model.TryCastTo<TModel>();

        internal void Apply<TActionResult>(ActionTestContext<TActionResult> actionTestContext)
        {
            this.ActionName = actionTestContext.ActionName;
            this.ActionCall = actionTestContext.ActionCall;
            this.ActionResult = actionTestContext.ActionResult;
            this.CaughtException = actionTestContext.CaughtException;
        }
    }
}
