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
        private IDictionary<Type, object> aggregatedDependencies;
        private string methodName;
        private MethodInfo method;
        private LambdaExpression methodCall;
        private object methodResult;
        private IEnumerable<object> methodAttributes;
        private object model;
        
        public object Component => this.component ??= this.ComponentConstructionDelegate();

        public IEnumerable<object> ComponentAttributes 
            => this.componentAttributes ??= Reflection.GetCustomAttributes(this.Component);

        public IDictionary<Type, object> AggregatedDependencies 
            => this.aggregatedDependencies ??= new Dictionary<Type, object>();

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

        public object MethodResult
        { 
            get => this.methodResult;

            set => this.methodResult = this.ConvertMethodResult(value);
        }

        public IEnumerable<object> MethodAttributes 
            => this.methodAttributes ??= Reflection.GetCustomAttributes(this.Method);

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

        protected virtual object ConvertMethodResult(object convertibleMethodResult) => convertibleMethodResult;
    }
}
