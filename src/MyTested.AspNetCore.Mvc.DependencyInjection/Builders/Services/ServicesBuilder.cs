namespace MyTested.AspNetCore.Mvc.Builders.Services
{
    using System;
    using Base;
    using Contracts.Services;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;
    using Utilities;

    /// <summary>
    /// Used for building services.
    /// </summary>
    public class ServicesBuilder : BaseTestBuilderWithComponent, IAndServicesBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ServicesBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndServicesBuilder With<TService>(TService service)
            where TService : class
        {
            var typeOfService = service != null
                ? service.GetType()
                : typeof(TService);

            if (this.TestContext.AggregatedServices.ContainsKey(typeOfService))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} is already registered.",
                    typeOfService.ToFriendlyTypeName()));
            }

            this.TestContext.AggregatedServices.Add(typeOfService, service);
            this.TestContext.ComponentConstructionDelegate = () => null;

            return this;
        }

        /// <inheritdoc />
        public IAndServicesBuilder WithNo<TService>()
            where TService : class
        {
            return this.With<TService>(null);
        }

        /// <inheritdoc />
        public IAndServicesBuilder WithSetupFor<TService>(Action<TService> scopedServiceSetup)
            where TService : class
        {
            CommonValidator.CheckForNullReference(scopedServiceSetup, nameof(scopedServiceSetup));
            ServiceValidator.ValidateScopedServiceLifetime<TService>(nameof(WithSetupFor));

            scopedServiceSetup(this.HttpContext.RequestServices.GetRequiredService<TService>());

            return this;
        }

        /// <inheritdoc />
        public IServicesBuilder AndAlso() => this;
    }
}
