namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Http;
    using Builders.Http;
    using Microsoft.AspNetCore.Http;
    using Utilities.Validators;

    /// <summary>
    /// Contains HTTP extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderHttpExtensions
    {
        /// <summary>
        /// Sets the <see cref="HttpContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="httpContext">Instance of <see cref="HttpContext"/> to set.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithHttpContext<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            HttpContext httpContext)
            where TBuilder : IBaseTestBuilder
        {
            CommonValidator.CheckForNullReference(httpContext, nameof(HttpContext));

            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.TestContext.HttpContext = httpContext;

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets the <see cref="HttpContext"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="httpContextSetup">Action setting the <see cref="HttpContext"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithHttpContext<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder, 
            Action<HttpContext> httpContextSetup)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            httpContextSetup(actualBuilder.TestContext.HttpContext);

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> on the tested component.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="httpRequest">Instance of <see cref="HttpRequest"/> to set.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithHttpRequest<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder, 
            HttpRequest httpRequest)
            where TBuilder : IBaseTestBuilder
        {
            CommonValidator.CheckForNullReference(httpRequest, nameof(HttpRequest));

            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.TestContext.HttpContextMock.CustomRequest = httpRequest;

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> on the tested controller.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="httpRequestBuilder">Action setting the <see cref="HttpRequest"/> by using <see cref="IHttpRequestBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithHttpRequest<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder, 
            Action<IHttpRequestBuilder> httpRequestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            var newHttpRequestBuilder = new HttpRequestBuilder(actualBuilder.HttpContext);
            httpRequestBuilder(newHttpRequestBuilder);
            newHttpRequestBuilder.ApplyTo(actualBuilder.HttpContext.Request);

            return actualBuilder.Builder;
        }
    }
}
