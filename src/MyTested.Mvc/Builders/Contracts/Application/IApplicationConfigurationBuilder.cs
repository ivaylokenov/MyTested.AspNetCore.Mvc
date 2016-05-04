namespace MyTested.Mvc.Builders.Contracts.Application
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Adds additional configuration to the tested application builder.
        /// </summary>
        /// <param name="config">Action for setting the configuration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithTestConfiguration(Action<IConfigurationBuilder> config);

        /// <summary>
        /// Adds additional services to the tested application's services collection.
        /// </summary>
        /// <param name="services">Action for service registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services);

        /// <summary>
        /// Adds additional middleware to the tested application builder.
        /// </summary>
        /// <param name="app">Action for middleware registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithApplication(Action<IApplicationBuilder> app);

        /// <summary>
        /// Adds additional routes to the tested application builder.
        /// </summary>
        /// <param name="routes">Action for route registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes);
    }
}
