namespace MyTested.AspNetCore.Mvc
{
    using Builders.Authentication;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Http;
    using Builders.Http;
    using System;

    public static class HttpRequestBuilderAuthenticationExtensions
    {
        /// <summary>
        /// Sets default authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        public static IAndHttpRequestBuilder WithAuthenticatedUser(this IHttpRequestBuilder httpRequestBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            actualHttpRequestBuilder.HttpContext.User = ClaimsPrincipalBuilder.DefaultAuthenticated;

            return actualHttpRequestBuilder;
        }

        /// <summary>
        /// Sets custom authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built request using the provided user builder.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <param name="userBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> by using <see cref="IClaimsPrincipalBuilder"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        public static IAndHttpRequestBuilder WithAuthenticatedUser(
            this IHttpRequestBuilder httpRequestBuilder,
            Action<IClaimsPrincipalBuilder> userBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            var newUserBuilder = new ClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            actualHttpRequestBuilder.HttpContext.User = newUserBuilder.GetClaimsPrincipal();

            return actualHttpRequestBuilder;
        }
    }
}
