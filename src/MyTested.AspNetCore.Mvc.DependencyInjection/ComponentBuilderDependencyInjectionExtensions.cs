namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Dependencies;
    using Builders.Dependencies;
    using Utilities.Extensions;

    /// <summary>
    /// Contains dependency injection extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderDependencyInjectionExtensions
    {
        /// <summary>
        /// Sets service dependencies on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="dependenciesBuilder">
        /// Action setting the service dependencies by using <see cref="IDependenciesBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithDependencies<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IDependenciesBuilder> dependenciesBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dependenciesBuilder(new DependenciesBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets constructor service dependencies on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="dependencies">Collection of service dependencies to inject into the component constructor.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithDependencies<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> dependencies)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dependencies
                .ForEach(dependency => actualBuilder
                    .WithDependencies(dependenciesBuilder => dependenciesBuilder
                        .With(dependency)));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets constructor service dependencies on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="dependencies">Service dependencies to inject into the component constructor.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithDependencies<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params object[] dependencies)
            where TBuilder : IBaseTestBuilder 
            => builder
                .WithDependencies(dependencies.AsEnumerable());
    }
}
