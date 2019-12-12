namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Security.Claims;
    using MyTested.AspNetCore.Mvc.Builders.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Base;

    public static class ComponentBuilderAuthenticationWithoutExtensions
    {
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string claimType, string value)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutUser(user => user.WithoutClaim(claimType, value));

        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Claim claim)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutUser(user => user.WithoutClaim(claim));

        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string role)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutUser(user => user.WithoutRole(role));

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
