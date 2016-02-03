namespace MyTested.Mvc.Builders.Contracts.Application
{
    using System;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Adds additional services to the tested application's services collection.
        /// </summary>
        /// <param name="services">Action for service registration.</param>
        /// <returns>The same application configuration builder.</returns>
        IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services);

        /// <summary>
        /// Adds additional middleware to the tested application builder.
        /// </summary>
        /// <param name="app">Action for middleware registration.</param>
        /// <returns>The same application configuration builder.</returns>
        IApplicationConfigurationBuilder WithApplication(Action<IApplicationBuilder> app);

        /// <summary>
        /// Adds additional routes to the tested application builder.
        /// </summary>
        /// <param name="routes">Action for route registration.</param>
        /// <returns>The same application configuration builder.</returns>
        IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes);
    }
}
