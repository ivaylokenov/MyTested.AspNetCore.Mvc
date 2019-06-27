namespace MyTested.AspNetCore.Mvc.Builders.Dependencies
{
    using System;
    using Base;
    using Contracts.Dependencies;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;
    using Utilities;

    /// <summary>
    /// Used for building service dependencies.
    /// </summary>
    public class DependenciesBuilder : BaseTestBuilderWithComponent, IAndDependenciesBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependenciesBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public DependenciesBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndDependenciesBuilder With<TService>(TService service)
            where TService : class
        {
            var typeOfService = service != null
                ? service.GetType()
                : typeof(TService);

            if (this.TestContext.AggregatedDependencies.ContainsKey(typeOfService))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} is already registered.",
                    typeOfService.ToFriendlyTypeName()));
            }

            this.TestContext.AggregatedDependencies.Add(typeOfService, service);
            this.TestContext.ComponentConstructionDelegate = () => null;

            return this;
        }

        /// <inheritdoc />
        public IAndDependenciesBuilder WithNo<TService>()
            where TService : class 
            => this.With<TService>(null);

        /// <inheritdoc />
        public IAndDependenciesBuilder WithSetupFor<TService>(Action<TService> scopedServiceSetup)
            where TService : class
        {
            CommonValidator.CheckForNullReference(scopedServiceSetup, nameof(scopedServiceSetup));
            ServiceValidator.ValidateScopedServiceLifetime<TService>(nameof(this.WithSetupFor));

            scopedServiceSetup(this.HttpContext.RequestServices.GetRequiredService<TService>());

            return this;
        }

        /// <inheritdoc />
        public IDependenciesBuilder AndAlso() => this;
    }
}
