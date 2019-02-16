namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Application
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Base;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public interface IApplicationConfigurationBuilder : IBaseInitializationBuilder<IAndApplicationConfigurationBuilder>
    {
        /// <summary>
        /// Adds additional services to the tested application.
        /// </summary>
        /// <param name="services">Action for service registration.</param>
        /// <returns>The same <see cref="IAndApplicationConfigurationBuilder"/>.</returns>
        IAndApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services);
        
        /// <summary>
        /// Adds additional middleware to the tested application.
        /// </summary>
        /// <param name="app">Action for middleware registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IAndApplicationConfigurationBuilder WithMiddleware(Action<IApplicationBuilder> app);

        /// <summary>
        /// Adds additional routes to the tested application.
        /// </summary>
        /// <param name="routes">Action for route registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IAndApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes);
    }
}
