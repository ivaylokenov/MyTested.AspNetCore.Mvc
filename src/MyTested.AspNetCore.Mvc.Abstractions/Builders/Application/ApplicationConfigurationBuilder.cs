namespace MyTested.AspNetCore.Mvc.Builders.Application
{
    using System;
    using Contracts.Application;
    using Internal.Application;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    /// <summary>
    /// Configures the tested application.
    /// </summary>
    public class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="startupType">Type of startup class.</param>
        public ApplicationConfigurationBuilder(Type startupType) 
            => TestApplication.StartupType = startupType;

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithTestConfiguration(Action<IConfigurationBuilder> config)
        {
            TestApplication.AdditionalConfiguration += config;
            return this;
        }

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services)
        {
            TestApplication.AdditionalServices += services;
            return this;
        }

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithConfiguration(Action<IApplicationBuilder> app)
        {
            TestApplication.AdditionalApplicationConfiguration += app;
            return this;
        }

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes)
        {
            TestApplication.AdditionalRouting += routes;
            return this;
        }

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithTestAssembly(object objectFromTestAssembly) 
            => this.WithTestAssembly(objectFromTestAssembly?.GetType());

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithTestAssembly(Type typeFromTestAssembly) 
            => this.WithTestAssembly(typeFromTestAssembly.GetTypeInfo().Assembly);

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithTestAssembly(Assembly testAssembly)
        {
            TestApplication.TestAssembly = testAssembly;
            return this;
        }

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithWebAssembly(object objectFromWebAssembly)
            => this.WithTestAssembly(objectFromWebAssembly?.GetType());

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithWebAssembly(Type typeFromWebAssembly)
            => this.WithTestAssembly(typeFromWebAssembly.GetTypeInfo().Assembly);

        /// <inheritdoc />
        public IApplicationConfigurationBuilder WithWebAssembly(Assembly webAssembly)
        {
            TestApplication.WebAssembly = webAssembly;
            return this;
        }
    }
}
