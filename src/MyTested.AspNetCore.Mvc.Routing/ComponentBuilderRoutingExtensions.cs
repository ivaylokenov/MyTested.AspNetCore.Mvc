namespace MyTested.AspNetCore.Mvc
{
    using Builders.Base;
    using Builders.Contracts.Base;
    using Internal.Application;
    using Internal.Routing;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;

    /// <summary>
    /// Contains HTTP extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderRoutingExtensions
    {
        /// <summary>
        /// Indicates that route values should be extracted from the provided method call expression.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithRouteData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            return builder.WithRouteData(null);
        }

        /// <summary>
        /// Indicates that route values should be extracted from the provided action call expression adding the given additional values.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="additionalRouteValues">Anonymous object containing route values.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithRouteData<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            object additionalRouteValues)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            actualBuilder.TestContext.PreMethodInvocationDelegate += () =>
            {
                var testContext = actualBuilder.TestContext;

                if (testContext.RouteDataMethodCall != null)
                {
                    testContext.RouteData = RouteExpressionParser.ResolveRouteData(
                        TestApplication.Router,
                        testContext.RouteDataMethodCall);
                }

                if (testContext.RouteData == null)
                {
                    testContext.RouteData = new RouteData();
                    testContext.RouteData.Routers.Add(TestApplication.Router);
                }
                
                testContext.RouteData.Values.Add(additionalRouteValues);
            };

            return actualBuilder.Builder;
        }
    }
}
