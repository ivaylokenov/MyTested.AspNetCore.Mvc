namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Http.ISession"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderSessionExtensions
    {
        /// <summary>
        /// Sets initial values to the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="sessionBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.ISession"/> values by using <see cref="ISessionBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<ISessionBuilder> sessionBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            sessionBuilder(new SessionBuilder(actualBuilder.TestContext.Session));

            return actualBuilder.Builder;
        }
    }
}
