namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.EntityFrameworkCore.DbContext"/>
    /// extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderEntityFrameworkCoreWithExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="entities">
        /// Initial values to add to the registered <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> entities)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithData(data => data
                    .WithEntities(entities));

        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="entities">
        /// Initial values to add to the registered <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params object[] entities)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithData(data => data
                    .WithEntities(entities));
        
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">
        /// Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.
        /// </param>
        /// <param name="dbContextBuilder">
        /// Action setting the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> by using <see cref="IWithDbContextBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithDbContextBuilder> dbContextBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dbContextBuilder(new WithDbContextBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
