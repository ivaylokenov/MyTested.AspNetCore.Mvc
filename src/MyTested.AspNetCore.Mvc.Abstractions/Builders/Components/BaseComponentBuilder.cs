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

    public abstract class BaseComponentBuilder<TComponent, TBuilder> : BaseTestBuilderWithComponentBuilder<TBuilder>
        where TComponent : class
        where TBuilder : IBaseTestBuilder
    {
        private bool isPreparedForTesting;

        public BaseComponentBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.TestContext.ComponentBuildDelegate += this.BuildComponentIfNotExists;
        }
        
        protected new MockedHttpContext HttpContext => this.TestContext.MockedHttpContext;

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

        protected abstract void PrepareComponentContext();

        protected abstract void PrepareComponent();
    }
}
