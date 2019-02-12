namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Application
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

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
        IApplicationConfigurationBuilder WithConfiguration(Action<IApplicationBuilder> app);

        /// <summary>
        /// Adds additional routes to the tested application builder.
        /// </summary>
        /// <param name="routes">Action for route registration.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes);

        /// <summary>
        /// Sets the test assembly for the tested application.
        /// </summary>
        /// <param name="objectFromTestAssembly">Instance object from the test assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithTestAssembly(object objectFromTestAssembly);

        /// <summary>
        /// Sets the test assembly for the tested application.
        /// </summary>
        /// <param name="typeFromTestAssembly">Type from the test assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithTestAssembly(Type typeFromTestAssembly);

        /// <summary>
        /// Sets the test assembly for the tested application.
        /// </summary>
        /// <param name="testAssembly">The assembly to set as test assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithTestAssembly(Assembly testAssembly);

        /// <summary>
        /// Sets the web assembly for the tested application.
        /// </summary>
        /// <param name="objectFromWebAssembly">Instance object from the web assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithWebAssembly(object objectFromWebAssembly);

        /// <summary>
        /// Sets the web assembly for the tested application.
        /// </summary>
        /// <param name="typeFromWebAssembly">Type from the web assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithWebAssembly(Type typeFromWebAssembly);

        /// <summary>
        /// Sets the web assembly for the tested application.
        /// </summary>
        /// <param name="webAssembly">The assembly to set as web assembly.</param>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder WithWebAssembly(Assembly webAssembly);
    }
}
