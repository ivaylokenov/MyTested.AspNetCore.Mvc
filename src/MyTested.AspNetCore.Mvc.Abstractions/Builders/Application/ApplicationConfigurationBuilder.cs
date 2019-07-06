namespace MyTested.AspNetCore.Mvc.Builders.Application
{
    using System;
    using Contracts.Application;
    using Internal.Application;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Base;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public class ApplicationConfigurationBuilder 
        : BaseInitializationBuilder<IAndApplicationConfigurationBuilder>, IAndApplicationConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="startupType">Type of startup class.</param>
        public ApplicationConfigurationBuilder(Type startupType) 
            => TestApplication.StartupType = startupType;

        /// <inheritdoc />
        protected override IAndApplicationConfigurationBuilder InitializationBuilder => this;

        /// <inheritdoc />
        public IAndApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services)
        {
            TestApplication.AdditionalServices += services;
            return this;
        }

        /// <inheritdoc />
        public IAndApplicationConfigurationBuilder WithMiddleware(Action<IApplicationBuilder> app)
        {
            TestApplication.AdditionalApplicationConfiguration += app;
            return this;
        }

        /// <inheritdoc />
        public IAndApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes)
        {
            TestApplication.AdditionalRouting += routes;
            return this;
        }
        
        /// <inheritdoc />
        public IApplicationConfigurationBuilder AndAlso() => this;
    }
}
