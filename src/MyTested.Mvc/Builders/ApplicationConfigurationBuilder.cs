namespace MyTested.Mvc.Builders
{
    using System;
    using Contracts.Application;
    using Internal.Application;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationBuilder" /> class.
        /// </summary>
        /// <param name="startupType">Type of startup class.</param>
        public ApplicationConfigurationBuilder(Type startupType)
        {
            TestApplication.StartupType = startupType;
        }

        /// <summary>
        /// Adds additional services to the tested application's services collection.
        /// </summary>
        /// <param name="services">Action for service registration.</param>
        /// <returns>The same application configuration builder.</returns>
        public IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services)
        {
            TestApplication.AdditionalServices = services;
            return this;
        }

        /// <summary>
        /// Adds additional middleware to the tested application builder.
        /// </summary>
        /// <param name="app">Action for middleware registration.</param>
        /// <returns>The same application configuration builder.</returns>
        public IApplicationConfigurationBuilder WithApplication(Action<IApplicationBuilder> app)
        {
            TestApplication.AdditionalApplicationConfiguration = app;
            return this;
        }

        /// <summary>
        /// Adds additional routes to the tested application builder.
        /// </summary>
        /// <param name="routes">Action for route registration.</param>
        /// <returns>The same application configuration builder.</returns>
        public IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes)
        {
            TestApplication.AdditionalRoutes = routes;
            return this;
        }
    }
}
