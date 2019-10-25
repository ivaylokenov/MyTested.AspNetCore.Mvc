namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Authentication;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Http;
    using Builders.Http;

    /// <summary>
    /// Contains authentication extension methods for <see cref="IHttpRequestBuilder"/>.
    /// </summary>
    public static class HttpRequestBuilderAuthenticationWithExtensions
    {
        /// <summary>
        /// Sets default authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        public static IAndHttpRequestBuilder WithUser(this IHttpRequestBuilder httpRequestBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            actualHttpRequestBuilder.HttpContext.User = WithClaimsPrincipalBuilder.DefaultAuthenticated;

            return actualHttpRequestBuilder;
        }

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            string username,
            IEnumerable<string> roles)
            => httpRequestBuilder
                .WithUser(user => user
                    .WithUsername(username)
                    .InRoles(roles));

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier (Id) and the provided username and optional roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            string username,
            params string[] roles)
            => httpRequestBuilder
                .WithUser(username, roles.AsEnumerable());

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with the provided identifier (Id), username and roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="identifier">Value of the identifier (Id) claim - <see cref="System.Security.Claims.ClaimTypes.NameIdentifier"/>.</param>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            string identifier,
            string username,
            IEnumerable<string> roles)
            => httpRequestBuilder
                .WithUser(user => user
                    .WithIdentifier(identifier)
                    .WithUsername(username)
                    .InRoles(roles));

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with the provided identifier (Id), username and optional roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="identifier">Value of the identifier (Id) claim - <see cref="System.Security.Claims.ClaimTypes.NameIdentifier"/>.</param>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="System.Security.Claims.ClaimTypes.Name"/>.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            string identifier,
            string username,
            params string[] roles)
            => httpRequestBuilder
                .WithUser(identifier, username, roles.AsEnumerable());

        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            IEnumerable<string> roles)
            => httpRequestBuilder
                .WithUser(user => user
                    .InRoles(roles));
        
        /// <summary>
        /// Sets an authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier (Id) and the provided username and roles.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same component builder.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            params string[] roles)
            => httpRequestBuilder
                .WithUser(user => user
                    .InRoles(roles));
        
        /// <summary>
        /// Sets custom authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request using the provided user builder.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="userBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> by using <see cref="IWithClaimsPrincipalBuilder"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        public static IAndHttpRequestBuilder WithUser(
            this IHttpRequestBuilder httpRequestBuilder,
            Action<IWithClaimsPrincipalBuilder> userBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            var newUserBuilder = new WithClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            actualHttpRequestBuilder.HttpContext.User = newUserBuilder.GetClaimsPrincipal();

            return actualHttpRequestBuilder;
        }
    }
}
