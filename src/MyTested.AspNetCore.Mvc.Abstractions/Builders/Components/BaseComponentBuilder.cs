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
    using Utilities.Extensions;
    using Utilities.Validators;

    public abstract partial class BaseComponentBuilder<TComponent, TTestContext, TBuilder> 
        : BaseTestBuilderWithComponentBuilder<TBuilder>
        where TComponent : class
        where TTestContext : ComponentTestContext
        where TBuilder : IBaseTestBuilder
    {
        private TTestContext testContext;
        private bool isPreparedForTesting;

        protected BaseComponentBuilder(TTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
            this.TestContext.ComponentBuildDelegate += this.BuildComponentIfNotExists;
            
            this.ValidateComponentType();
        }

        public new TTestContext TestContext
        {
            get => this.testContext;

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

        protected bool SkipComponentActivation { get; set; }
        
        protected void BuildComponentIfNotExists()
        {
            if (!this.isPreparedForTesting)
            {
                this.PrepareComponentContext();
            }

            var component = this.TestContext.Component;
            if (component == null)
            {
                component = this.TryExtractComponentFromExecution();

                var explicitDependenciesAreSet = this.TestContext.AggregatedDependencies.Any();
                if (explicitDependenciesAreSet)
                {
                    var executionComponent = component;

                    // Custom dependencies are set, try create instance with them.
                    component = Reflection.TryCreateInstance<TComponent>(this.TestContext.AggregatedDependencies);

                    if (executionComponent != null)
                    {
                        // Copy the public properties from the execution component to the newly created instance.
                        Reflection.CopyProperties(executionComponent, component);

                        this.SkipComponentActivation = true;
                    }
                }
                else if (component == null)
                {
                    // No execution component, no custom dependencies set, try create instance with the component factory.
                    component = this.TryCreateComponentWithFactory();

                    if (component != null)
                    {
                        this.SkipComponentActivation = true;
                    }
                    else
                    {
                        // No component from the factory, try create instance with the global services.
                        component = TestHelper.TryCreateInstance<TComponent>();
                    }
                }

                if (component == null && !explicitDependenciesAreSet)
                {
                    // No component at this point, try to create one with default constructor.
                    component = Reflection.TryFastCreateInstance<TComponent>();
                }

                if (component == null)
                {
                    var friendlyServiceNames = this.TestContext.AggregatedDependencies
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyServices = string.Join(", ", friendlyServiceNames);

                    throw new UnresolvedServicesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TComponent).ToFriendlyTypeName(),
                        this.TestContext.AggregatedDependencies.Count == 0 ? "no" : $"{joinedFriendlyServices} as"));
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
                throw new InvalidOperationException($"{typeof(TComponent).ToFriendlyTypeName()} is not recognized as a valid {this.ComponentName} type. Classes decorated with 'Non{this.ComponentName.CapitalizeAndJoin()}Attribute' are not considered as passable {this.ComponentName}s. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file in order to enable proper {this.ComponentName} discovery. If your type is still not recognized, you may manually add it in the application part manager by using the 'AddMvc().PartManager.ApplicationParts.Add(applicationPart))' method.");
            }
        }
        
        protected abstract TComponent TryCreateComponentWithFactory();

        protected abstract void ActivateComponent();

        protected virtual TComponent TryExtractComponentFromExecution() => null;

        private void PrepareComponent()
        {
            if (!this.SkipComponentActivation)
            {
                this.ActivateComponent();
            }

            this.TestContext.ComponentPreparationDelegate?.Invoke();

            this.componentSetupAction?.Invoke(this.TestContext.ComponentAs<TComponent>());
        }
    }
}
