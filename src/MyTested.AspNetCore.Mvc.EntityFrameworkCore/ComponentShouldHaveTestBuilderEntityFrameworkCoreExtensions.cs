namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.EntityFrameworkCore.DbContext"/> extension methods for <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentShouldHaveTestBuilderEntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Tests whether the action saves entities in the <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="dbContextTestBuilder">Action containing all assertions for the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entities.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder Data<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IDbContextTestBuilder> dbContextTestBuilder)
            where TBuilder : IBaseTestBuilder
            => builder.DbContext(dbContextTestBuilder);

        /// <summary>
        /// Tests whether the action saves entities in the <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="dbContextTestBuilder">Action containing all assertions for the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entities.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder DbContext<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IDbContextTestBuilder> dbContextTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            dbContextTestBuilder(new DbContextTestBuilder(actualBuilder.TestContext));
            
            return actualBuilder.Builder;
        }
    }
}
