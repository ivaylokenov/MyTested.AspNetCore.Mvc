namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System.Collections.Generic;
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TRouteValuesResultTestBuilder">Type of route values result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithRouteValuesResult<TRouteValuesResultTestBuilder>
        : IBaseTestBuilderWithUrlHelperResult<TRouteValuesResultTestBuilder>
        where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder WithRouteName(string routeName);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteKey(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value.
        /// </summary>
        /// <typeparam name="TRouteValue">Type of the route value.</typeparam>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value of the given type.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValueOfType<TRouteValue>();

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route value of the given type with the provided key.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValueOfType<TRouteValue>(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValue(string key, object value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValues(object routeValues);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same route values <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRouteValuesResultTestBuilder ContainingRouteValues(IDictionary<string, object> routeValues);
    }
}
