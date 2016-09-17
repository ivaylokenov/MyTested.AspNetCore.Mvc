namespace MyTested.AspNetCore.Mvc
{
    using System;
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
        /// Sets default authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built component with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithAuthenticatedUser<TBuilder>(this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.HttpContext.User = ClaimsPrincipalBuilder.DefaultAuthenticated;

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets custom authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the built component using the provided user builder.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="userBuilder">Action setting the <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> by using <see cref="IClaimsPrincipalBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithAuthenticatedUser<TBuilder>(
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
