namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Authentication;
    using Builders.Base;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Base;

    /// <summary>
    /// Contains authentication extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderAuthenticationExtensions
    {
        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="username">
        /// Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.
        /// </param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string username,
            IEnumerable<string> roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(user => user
                .WithUsername(username)
                .InRoles(roles));

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with "TestId" identifier (Id) and the provided username and optional roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="username">
        /// Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.
        /// </param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string username, 
            params string[] roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(username, roles.AsEnumerable());

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with the provided identifier (Id), username and roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="identifier">
        /// Value of the identifier (Id) claim - <see cref="System.Security.Claims.ClaimTypes.NameIdentifier"/>.
        /// </param>
        /// <param name="username">
        /// Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.
        /// </param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string identifier,
            string username,
            IEnumerable<string> roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(user => user
                .WithIdentifier(identifier)
                .WithUsername(username)
                .InRoles(roles));

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with the provided identifier (Id), username and optional roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="identifier">
        /// Value of the identifier (Id) claim - <see cref="System.Security.Claims.ClaimTypes.NameIdentifier"/>.
        /// </param>
        /// <param name="username">
        /// Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.
        /// </param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string identifier, 
            string username,
            params string[] roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(identifier, username, roles.AsEnumerable());
        
        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(user => user.InRoles(roles));
        
        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] roles)
            where TBuilder : IBaseTestBuilder
            => builder.WithUser(user => user.InRoles(roles));
        
        /// <summary>
        /// Sets default authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component with "TestId" identifier (Id) and "TestUser" username.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.HttpContext.User = ClaimsPrincipalBuilder.DefaultAuthenticated;

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets custom authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component using the provided user builder.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="userBuilder">
        /// Action setting the <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> by using <see cref="IClaimsPrincipalBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IClaimsPrincipalBuilder> userBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            var newUserBuilder = new ClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            actualBuilder.HttpContext.User = newUserBuilder.GetClaimsPrincipal();

            return actualBuilder.Builder;
        }
    }
}
