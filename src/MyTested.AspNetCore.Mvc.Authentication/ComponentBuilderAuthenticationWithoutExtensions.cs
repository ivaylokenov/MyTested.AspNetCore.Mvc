namespace MyTested.AspNetCore.Mvc
{
    using MyTested.AspNetCore.Mvc.Builders.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Base;
    using System;

    public static class ComponentBuilderAuthenticationWithoutExtensions
    {
        public static TBuilder WithoutUser<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutClaimsPrincipalBuilder> userBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            var newUserBuilder = new WithoutClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            actualBuilder.HttpContext.User = newUserBuilder.GetClaimsPrincipal();

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
