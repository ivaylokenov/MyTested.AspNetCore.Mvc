namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Services;
    using Builders.Services;
    using Utilities.Extensions;

    /// <summary>
    /// Contains dependency injection extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderDependencyInjectionExtensions
    {
        /// <summary>
        /// Sets services on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="servicesBuilder">Action setting the services by using <see cref="IServicesBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithServices<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IServicesBuilder> servicesBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            servicesBuilder(new ServicesBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets constructor services on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="services">Collection of service dependencies to inject into the component constructor.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithServices<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> services)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            services.ForEach(service => actualBuilder.WithServices(serviceBuilder => serviceBuilder.With(service)));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets constructor services on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="services">Services to inject into the component constructor.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithServices<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params object[] services)
            where TBuilder : IBaseTestBuilder
        {
            return builder.WithServices(services.AsEnumerable());
        }
    }
}
