namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Validators;

    public abstract class ComponentTestContext : HttpTestContext
    {
        private object component;
        private IEnumerable<object> controllerAttributes;
        private string methodName;
        private MethodInfo method;
        private LambdaExpression methodCall;
        private IEnumerable<object> methodAttributes;
        private object model;
        private RouteData expressionRouteData;

        public object Component
        {
            get
            {
                if (this.component == null)
                {
                    this.component = this.ComponentConstruction();
                }

                return this.component;
            }
        }

        public IEnumerable<object> ComponentAttributes
        {
            get
            {
                if (this.controllerAttributes == null)
                {
                    this.controllerAttributes = Reflection.GetCustomAttributes(this.Component);
                }

                return this.controllerAttributes;
            }
        }

        public string MethodName
        {
            get
            {
                return this.methodName;
            }

            protected set
            {
                CommonValidator.CheckForNotWhiteSpaceString(value, nameof(this.MethodName));
                this.methodName = value;
            }
        }

        public MethodInfo Method
        {
            get
            {
                if (this.method == null)
                {
                    this.method = ExpressionParser.GetMethodInfo(this.methodCall);
                }

                return this.method;
            }
        }

        public LambdaExpression MethodCall
        {
            get
            {
                return this.methodCall;
            }

            protected set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.MethodCall));
                this.methodCall = value;
            }
        }

        public object MethodResult { get; set; }

        public IEnumerable<object> MethodAttributes
        {
            get
            {
                if (this.methodAttributes == null)
                {
                    this.methodAttributes = Reflection.GetCustomAttributes(this.Method);
                }

                return this.methodAttributes;
            }
        }
        public Exception CaughtException { get; protected set; }

        public object Model
        {
            get
            {
                if (this.model == null)
                {
                    return this.MethodResult;
                }

                return this.model;
            }

            set
            {
                this.model = value;
            }
        }
        
        protected Func<object> ComponentConstruction { get; set; }
    }
}
