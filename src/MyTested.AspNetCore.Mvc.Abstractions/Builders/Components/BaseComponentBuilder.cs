namespace MyTested.AspNetCore.Mvc.Builders.Components
{
    using System;
    using System.Linq;
    using Base;
    using Contracts.Base;
    using Exceptions;
    using Internal;
    using Internal.Http;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities;
    using Utilities.Validators;

    public abstract class BaseComponentBuilder<TComponent, TTestContext, TBuilder> : BaseTestBuilderWithComponentBuilder<TBuilder>
        where TComponent : class
        where TTestContext : ComponentTestContext
        where TBuilder : IBaseTestBuilder
    {
        private TTestContext testContext;
        private bool isPreparedForTesting;

        public BaseComponentBuilder(TTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
            this.TestContext.ComponentBuildDelegate += this.BuildComponentIfNotExists;

#if NETSTANDARD1_6
            this.ValidateComponentType();
#endif
        }

        public new TTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }
        
        protected new HttpContextMock HttpContext => this.TestContext.HttpContextMock;

        protected HttpRequest HttpRequest => this.HttpContext.Request;

        protected IServiceProvider Services => this.HttpContext.RequestServices;

        protected TComponent Component
        {
            get
            {
                this.TestContext.ComponentBuildDelegate?.Invoke();
                return this.TestContext.ComponentAs<TComponent>();
            }
        }

        protected abstract string ComponentName { get; }

        protected abstract bool IsValidComponent { get; }
        
        protected virtual void BuildComponentIfNotExists()
        {
            if (!this.isPreparedForTesting)
            {
                this.PrepareComponentContext();
            }

            var component = this.TestContext.Component;
            if (component == null)
            {
                var explicitDependenciesAreSet = this.TestContext.AggregatedServices.Any();
                if (explicitDependenciesAreSet)
                {
                    // custom dependencies are set, try create instance with them
                    component = Reflection.TryCreateInstance<TComponent>(this.TestContext.AggregatedServices);
                }
                else
                {
                    // no custom dependencies are set, try create instance with the global services
                    component = TestHelper.TryCreateInstance<TComponent>();
                }

                if (component == null && !explicitDependenciesAreSet)
                {
                    // no component at this point, try to create one with default constructor
                    component = Reflection.TryFastCreateInstance<TComponent>();
                }

                if (component == null)
                {
                    var friendlyServiceNames = this.TestContext.AggregatedServices
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyServices = string.Join(", ", friendlyServiceNames);

                    throw new UnresolvedServicesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TComponent).ToFriendlyTypeName(),
                        this.TestContext.AggregatedServices.Count == 0 ? "no" : $"{joinedFriendlyServices} as"));
                }

                this.TestContext.ComponentConstructionDelegate = () => component;
            }

            if (!this.isPreparedForTesting)
            {
                this.PrepareComponent();
                this.isPreparedForTesting = true;
            }
        }

        protected void ValidateComponentType()
        {
            if (!this.IsValidComponent)
            {
                throw new InvalidOperationException($"{typeof(TComponent).ToFriendlyTypeName()} is not a valid {this.ComponentName} type.");
            }
        }

        protected abstract void PrepareComponentContext();

        protected abstract void PrepareComponent();
    }
}
