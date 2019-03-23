namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Http;
    using Builders.Contracts.Base;
    using Builders.Base;
    using Builders.Http;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> extension methods for
    /// <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentShouldHaveTestBuilderHttpExtensions
    {
        /// <summary>
        /// Tests whether the component applies additional features to the <see cref="Microsoft.AspNetCore.Http.HttpResponse"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="httpResponseTestBuilder">Builder for testing the <see cref="Microsoft.AspNetCore.Http.HttpResponse"/>.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder HttpResponse<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IHttpResponseTestBuilder> httpResponseTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            httpResponseTestBuilder(new HttpResponseTestBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
