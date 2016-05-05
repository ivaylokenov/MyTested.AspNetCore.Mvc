namespace MyTested.Mvc.Builders.Contracts.Routes
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing route details.
    /// </summary>
    public interface IResolvedRouteTestBuilder
    {
        /// <summary>
        /// Tests whether the resolved <see cref="Microsoft.AspNetCore.Routing.RouteValueDictionary"/> contains route value with the provided key.
        /// </summary>
        /// <param name="key">Expected route value key.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToRouteValue(string key);

        /// <summary>
        /// Tests whether the resolved <see cref="Microsoft.AspNetCore.Routing.RouteValueDictionary"/> contains route value with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Expected route value key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToRouteValue(string key, object value);

        /// <summary>
        /// Tests whether the resolved <see cref="Microsoft.AspNetCore.Routing.RouteValueDictionary"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Anonymous object of route values.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToRouteValues(object routeValues);

        /// <summary>
        /// Tests whether the resolved <see cref="Microsoft.AspNetCore.Routing.RouteValueDictionary"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Dictionary of route values.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToRouteValues(IDictionary<string, object> routeValues);

        /// <summary>
        /// Tests whether the resolved route contains data token with the provided key.
        /// </summary>
        /// <param name="key">Expected data token key.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToDataToken(string key);

        /// <summary>
        /// Tests whether the resolved route contains data token with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Expected data token key.</param>
        /// <param name="value">Expected data token value.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToDataToken(string key, object value);

        /// <summary>
        /// Tests whether the resolved route contains the provided data tokens.
        /// </summary>
        /// <param name="dataTokens">Anonymous object of data tokens.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToDataTokens(object dataTokens);

        /// <summary>
        /// Tests whether the resolved route contains the provided data tokens.
        /// </summary>
        /// <param name="dataTokens">Dictionary of data tokens.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToDataTokens(IDictionary<string, object> dataTokens);

        /// <summary>
        /// Tests whether the resolved route has valid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToValidModelState();

        /// <summary>
        /// Tests whether the resolved route has invalid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToInvalidModelState(int? withNumberOfErrors = null);
    }
}
