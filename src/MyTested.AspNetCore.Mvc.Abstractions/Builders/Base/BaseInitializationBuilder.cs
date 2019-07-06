namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using System.Reflection;
    using Contracts.Base;
    using Internal.Configuration;
    using Internal.Server;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Base class for all test builders with initialization builders.
    /// </summary>
    /// <typeparam name="TBuilder">Base initialization builder.</typeparam>
    public abstract class BaseInitializationBuilder<TBuilder> : IBaseInitializationBuilder<TBuilder>
        where TBuilder : IBaseInitializationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseInitializationBuilder{TBuilder}"/> class.
        /// </summary>
        protected BaseInitializationBuilder()
            => TestWebServer.ResetConfigurationAndAssemblies();

        /// <summary>
        /// Gets the initialization builder.
        /// </summary>
        /// <value>Base initialization builder.</value>
        protected abstract TBuilder InitializationBuilder { get; }

        /// <inheritdoc />
        public TBuilder WithConfiguration(Action<IConfigurationBuilder> config)
        {
            ServerTestConfiguration.AdditionalConfiguration += config;
            return this.InitializationBuilder;
        }

        /// <inheritdoc />
        public TBuilder WithTestAssembly(object objectFromTestAssembly)
            => this.WithTestAssembly(objectFromTestAssembly?.GetType());

        /// <inheritdoc />
        public TBuilder WithTestAssembly(Type typeFromTestAssembly)
            => this.WithTestAssembly(typeFromTestAssembly.GetTypeInfo().Assembly);

        /// <inheritdoc />
        public TBuilder WithTestAssembly(Assembly testAssembly)
        {
            TestWebServer.TestAssembly = testAssembly;
            return this.InitializationBuilder;
        }

        /// <inheritdoc />
        public TBuilder WithWebAssembly(object objectFromWebAssembly)
            => this.WithTestAssembly(objectFromWebAssembly?.GetType());

        /// <inheritdoc />
        public TBuilder WithWebAssembly(Type typeFromWebAssembly)
            => this.WithTestAssembly(typeFromWebAssembly.GetTypeInfo().Assembly);

        /// <inheritdoc />
        public TBuilder WithWebAssembly(Assembly webAssembly)
        {
            TestWebServer.WebAssembly = webAssembly;
            return this.InitializationBuilder;
        }
    }
}
