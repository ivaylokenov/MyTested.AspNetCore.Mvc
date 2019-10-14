namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="sessionBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.ISession"/> values by using <see cref="IWithSessionBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithSessionBuilder> sessionBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            sessionBuilder(new WithSessionBuilder(actualBuilder.TestContext.Session));

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Clears the whole http <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutSession(session => session.WithoutAllEntries());

        /// <summary>
        /// Remove session key from the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="key">Session key to remove.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutSession(session => session.WithoutEntry(key));

        /// <summary>
        /// Remove collection of session key entries from the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Session key entries to remove.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutSession(session => session.WithoutEntries(keys));

        /// <summary>
        /// Remove collection of session key entries from the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Session key entries to remove.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutSession(session => session.WithoutEntries(keys));

        /// <summary>
        /// Remove session key from the HTTP <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="sessionBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.ISession"/> values by using <see cref="IWithoutSessionBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutSession<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutSessionBuilder> sessionBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            sessionBuilder(new WithoutSessionBuilder(actualBuilder.TestContext.Session));

            return actualBuilder.Builder;
        }
    }
}
