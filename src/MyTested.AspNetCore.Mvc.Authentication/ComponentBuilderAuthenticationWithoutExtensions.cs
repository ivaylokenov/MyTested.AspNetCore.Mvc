namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Security.Claims;
    using MyTested.AspNetCore.Mvc.Builders.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Base;

    /// <summary>
    /// Contains authentication extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderAuthenticationWithoutExtensions
    {
        /// <summary>
        /// Removes the <see cref="Claim"/> that have the provided claimType and value from <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="claimType">The type of the claim that will be removed.</param>
        /// <param name="value">The value of the claim that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string claimType, string value)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutUser(user => user.WithoutClaim(claimType, value));

        /// <summary>
        /// Removes the provided <see cref="Claim"/> from <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="claim">The claim that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Claim claim)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutUser(user => user
                .WithoutClaim(claim));

        /// <summary>
        /// Removes the <see cref="Claim"/> that have the provided role from <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="role">The role of the claim that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string role)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutUser(user => user
                .WithoutRole(role));

        /// <summary>
        /// Sets custom authenticated <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> to the
        /// built component using the provided user builder.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="userBuilder">
        /// Action setting the <see cref="Microsoft.AspNetCore.Http.HttpContext.User"/> by using <see cref="IWithoutClaimsPrincipalBuilder"/>.
        /// </param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutClaimsPrincipalBuilder> userBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            var newUserBuilder = new WithoutClaimsPrincipalBuilder(actualBuilder.HttpContext.User);
            userBuilder(newUserBuilder);
            actualBuilder.HttpContext.User = newUserBuilder.GetClaimsPrincipalBasedOnClaimsOnly();

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets the default authenticated claims principal with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;
            actualBuilder.HttpContext.User = BaseClaimsPrincipalUserBuilder.DefaultAuthenticated;

            return actualBuilder.Builder;
        }
    }
}
