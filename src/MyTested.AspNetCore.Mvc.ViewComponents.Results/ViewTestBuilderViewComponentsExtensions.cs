namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ViewComponentResults;
    using Builders.ViewComponentResults;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Utilities;

    /// <summary>
    /// Contains extension methods for <see cref="IViewTestBuilder" />.
    /// </summary>
    public static class ViewTestBuilderViewComponentsExtensions
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <param name="viewEngine">View engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngine(
            this IViewTestBuilder viewTestBuilder,
            IViewEngine viewEngine)
        {
            var actualBuilder = GetActualBuilder(viewTestBuilder);

            var actualViewEngine = actualBuilder.ViewResult.ViewEngine;
            if (viewEngine != actualViewEngine)
            {
                throw ViewViewComponentResultAssertionException
                    .ForViewEngineEquality(actualBuilder.TestContext.ExceptionMessagePrefix);
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngineOfType<TViewEngine>(
            this IViewTestBuilder viewTestBuilder)
            where TViewEngine : IViewEngine
            => WithViewEngineOfType(viewTestBuilder, typeof(TViewEngine));

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <param name="viewEngineType"></param>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngineOfType(
            this IViewTestBuilder viewTestBuilder,Type viewEngineType)
        {
            var actualBuilder = GetActualBuilder(viewTestBuilder);

            var actualViewEngineType = actualBuilder.ViewResult?.ViewEngine?.GetType();

            if (actualViewEngineType == null
                || Reflection.AreDifferentTypes(viewEngineType, actualViewEngineType))
            {
                throw ViewViewComponentResultAssertionException.ForViewEngineType(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    viewEngineType.ToFriendlyTypeName(),
                    actualViewEngineType.ToFriendlyTypeName());
            }

            return actualBuilder;
        }

        private static ViewTestBuilder GetActualBuilder(IViewTestBuilder viewTestBuilder)
            => (ViewTestBuilder)viewTestBuilder;
    }
}
