namespace MyTested.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithRouteValuesResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has specific route name.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder WithRouteName<TRouteValuesResultTestBuilder>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            string routeName)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);
            
            RouteActionResultValidator.ValidateRouteName(
                actualBuilder.TestContext.MethodResult,
                routeName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route key.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteKey<TRouteValuesResultTestBuilder>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            string key)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValue(
                actualBuilder.TestContext.MethodResult,
                key,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValue<TRouteValuesResultTestBuilder, TRouteValue>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            TRouteValue value)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValue(
                actualBuilder.TestContext.MethodResult,
                value,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value of the given type.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValueOfType<TRouteValuesResultTestBuilder, TRouteValue>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                actualBuilder.TestContext.MethodResult,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value of the given type with the provided key.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValueOfType<TRouteValuesResultTestBuilder, TRouteValue>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            string key)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                actualBuilder.TestContext.MethodResult,
                key,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route key and value.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValue<TRouteValuesResultTestBuilder>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            string key,
            object value)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValue(
                actualBuilder.TestContext.MethodResult,
                key,
                value,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided route values.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValues<TRouteValuesResultTestBuilder>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            object routeValues)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithRouteValuesResult
                .ContainingRouteValues(new RouteValueDictionary(routeValues));

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided route values.
        /// </summary>
        /// <param name="baseTestBuilderWithRouteValuesResult">
        /// Instance of <see cref="IBaseTestBuilderWithRouteValuesResult{TRouteValuesResultTestBuilder}"/> type.
        /// </param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRouteValuesResultTestBuilder ContainingRouteValues<TRouteValuesResultTestBuilder>(
            this IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult,
            IDictionary<string, object> routeValues)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRouteValuesResult);

            RouteActionResultValidator.ValidateRouteValues(
                actualBuilder.TestContext.MethodResult,
                routeValues,
                actualBuilder.IncludeCountCheck,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }
        
        private static IBaseTestBuilderWithRouteValuesResultInternal<TRouteValuesResultTestBuilder>
            GetActualBuilder<TRouteValuesResultTestBuilder>(
                IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder> baseTestBuilderWithRouteValuesResult)
            where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithRouteValuesResultInternal<TRouteValuesResultTestBuilder>)baseTestBuilderWithRouteValuesResult;
    }
}
