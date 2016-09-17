namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Contains <see cref="DbContext"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderEntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="DbContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="dbContextBuilder">Action setting the <see cref="DbContext"/> by using <see cref="IDbContextBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithDbContext<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IDbContextBuilder> dbContextBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dbContextBuilder(new DbContextBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
