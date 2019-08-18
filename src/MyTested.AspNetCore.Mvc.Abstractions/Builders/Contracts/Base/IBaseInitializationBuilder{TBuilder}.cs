namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using System;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Base interface for all test builders with initialization builders.
    /// </summary>
    /// <typeparam name="TBuilder">Base initialization builder.</typeparam>
    public interface IBaseInitializationBuilder<TBuilder> : IBaseInitializationBuilder
        where TBuilder : IBaseInitializationBuilder
    {
        /// <summary>
        /// Adds additional configuration.
        /// </summary>
        /// <param name="config">Action for setting the configuration.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithConfiguration(Action<IConfigurationBuilder> config);

        /// <summary>
        /// Sets the test assembly.
        /// </summary>
        /// <param name="objectFromTestAssembly">Instance object from the test assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithTestAssembly(object objectFromTestAssembly);

        /// <summary>
        /// Sets the test assembly.
        /// </summary>
        /// <param name="typeFromTestAssembly">Type from the test assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithTestAssembly(Type typeFromTestAssembly);

        /// <summary>
        /// Sets the test assembly.
        /// </summary>
        /// <param name="testAssembly">The assembly to set as test assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithTestAssembly(Assembly testAssembly);

        /// <summary>
        /// Sets the web assembly.
        /// </summary>
        /// <param name="objectFromWebAssembly">Instance object from the web assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithWebAssembly(object objectFromWebAssembly);

        /// <summary>
        /// Sets the web assembly.
        /// </summary>
        /// <param name="typeFromWebAssembly">Type from the web assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithWebAssembly(Type typeFromWebAssembly);

        /// <summary>
        /// Sets the web assembly.
        /// </summary>
        /// <param name="webAssembly">The assembly to set as web assembly.</param>
        /// <returns>The same <see cref="IBaseInitializationBuilder"/>.</returns>
        TBuilder WithWebAssembly(Assembly webAssembly);
    }
}
