namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Utilities;
    using Utilities.Validators;
    using Utilities.Extensions;

    public abstract class ComponentTestContext : HttpTestContext
    {
        private object component;
        private IEnumerable<object> componentAttributes;
        private IDictionary<Type, object> aggregatedServices;
        private string methodName;
        private MethodInfo method;
        private LambdaExpression methodCall;
        private IEnumerable<object> methodAttributes;
        private object model;
        
        public object Component
        {
            get
            {
                if (this.component == null)
                {
                    this.component = this.ComponentConstructionDelegate();
                }

                return this.component;
            }
        }

        public IEnumerable<object> ComponentAttributes
        {
            get
            {
                if (this.componentAttributes == null)
                {
                    this.componentAttributes = Reflection.GetCustomAttributes(this.Component);
                }

                return this.componentAttributes;
            }
        }

        public IDictionary<Type, object> AggregatedServices
        {
            get
            {
                if (this.aggregatedServices == null)
                {
                    this.aggregatedServices = new Dictionary<Type, object>();
                }

                return this.aggregatedServices;
            }
        }

        public string MethodName
        {
            get => this.methodName;

            set
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
            get => this.methodCall;

            set
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

        public Exception CaughtException { get; set; }

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

            set => this.model = value;
        }

        public Func<object> ComponentConstructionDelegate { get; set; }

        public Action ComponentBuildDelegate { get; set; }
        
        public Action ComponentPreparationDelegate { get; set; }

        public Action PreMethodInvocationDelegate { get; set; }
        
        public TComponent ComponentAs<TComponent>()
            where TComponent : class => this.Component as TComponent;

        public TMethodResult MethodResultAs<TMethodResult>() => this.MethodResult.TryCastTo<TMethodResult>();

        public TException CaughtExceptionAs<TException>()
            where TException : Exception => this.CaughtException as TException;

        public TModel ModelAs<TModel>() => this.Model.TryCastTo<TModel>();
        
        public void Apply<TMethodResult>(InvocationTestContext<TMethodResult> invocationTestContext)
        {
            this.MethodName = invocationTestContext.MethodName;
            this.MethodCall = invocationTestContext.MethodCall;
            this.MethodResult = invocationTestContext.MethodResult;
            this.CaughtException = invocationTestContext.CaughtException;
        }
    }
}
