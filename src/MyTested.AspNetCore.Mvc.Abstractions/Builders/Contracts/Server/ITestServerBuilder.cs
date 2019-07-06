namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Server
{
    using System;
    using Base;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Builders.Contracts.Application;

    /// <summary>
    /// Configures the test server.
    /// </summary>
    public interface ITestServerBuilder : IBaseInitializationBuilder<IAndTestServerBuilder>
    {
        /// <summary>
        /// Adds additional services to the test server.
        /// </summary>
        /// <param name="services">Action for service registration.</param>
        /// <returns>The same <see cref="IAndTestServerBuilder"/>.</returns>
        IAndTestServerBuilder WithServices(Action<IServiceCollection> services);

        /// <summary>
        /// Specifies the Startup class from which the test application is bootstrapped.
        /// </summary>
        /// <typeparam name="TStartup">Startup class to bootstrap the test application from.</typeparam>
        /// <returns>The same <see cref="IAndTestServerBuilder"/>.</returns>
        void WithStartup<TStartup>() where TStartup : class;

        /// <summary>
        /// Specifies the Startup class from which the test application is bootstrapped.
        /// </summary>
        /// <typeparam name="TStartup">Startup class to bootstrap the test application from.</typeparam>
        /// <param name="applicationBuilder">Builder of <see cref="IApplicationConfigurationBuilder"/> type which configures the tested application.</param>
        /// <returns>The same <see cref="IAndTestServerBuilder"/>.</returns>
        void WithStartup<TStartup>(Action<IApplicationConfigurationBuilder> applicationBuilder)
            where TStartup : class;
    }
}
