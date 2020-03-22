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
        {
            get => this.componentAttributes ??= Reflection.GetCustomAttributes(this.Component);
            private set => this.componentAttributes = value;
        }

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
        {
            get => this.methodAttributes ??= Reflection.GetCustomAttributes(this.Method);
            private set => this.methodAttributes = value;
        }

        public Exception CaughtException { get; set; }

        public object Model
        {
            get => this.model ?? this.MethodResult;
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

        public void IncludeInheritedComponentAttributes()
            => this.ComponentAttributes = Reflection.GetCustomAttributes(this.Component, true);

        public void IncludeInheritedMethodAttributes()
            => this.MethodAttributes = Reflection.GetCustomAttributes(this.Method, true);

        protected virtual object ConvertMethodResult(object convertibleMethodResult) => convertibleMethodResult;
    }
}
