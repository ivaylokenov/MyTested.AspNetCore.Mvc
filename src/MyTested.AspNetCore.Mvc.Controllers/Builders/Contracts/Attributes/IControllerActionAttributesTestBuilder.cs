namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base interface for all controller action attribute test builders.
    /// </summary>
    /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type for common methods.</typeparam>
    public interface IControllerActionAttributesTestBuilder<TAttributesTestBuilder> : IBaseAttributesTestBuilder<TAttributesTestBuilder>
        where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
        /// </summary>
        /// <param name="template">Expected overridden route template of the controller.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
        /// </summary>
        /// <param name="routeAttributeBuilder">Expected <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder ChangingRouteTo(Action<IRouteAttributeTestBuilder> routeAttributeBuilder);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.AreaAttribute"/>.
        /// </summary>
        /// <param name="areaName">Expected area name.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingArea(string areaName);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ConsumesAttribute"/>.
        /// </summary>
        /// <param name="ofContentType">Expected content type.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingConsumption(string ofContentType);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ConsumesAttribute"/>.
        /// </summary>
        /// <param name="ofContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingConsumption(IEnumerable<string> ofContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ConsumesAttribute"/>.
        /// </summary>
        /// <param name="ofContentType">Expected content type.</param>
        /// <param name="withOtherContentTypes">Expected other content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingConsumption(string ofContentType, params string[] withOtherContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="ofContentType">Expected content type.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(string ofContentType);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="ofContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(IEnumerable<string> ofContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="ofContentType">Expected content type.</param>
        /// <param name="withOtherContentTypes">Expected other content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(string ofContentType, params string[] withOtherContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="withType">Expected type.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(Type withType);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="withType">Expected type.</param>
        /// <param name="withContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(Type withType, IEnumerable<string> withContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="withType">Expected type.</param>
        /// <param name="withContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(Type withType, params string[] withContentTypes);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <param name="producesAttributeBuilder">Expected <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingProduction(Action<IProducesAttributeTestBuilder> producesAttributeBuilder);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.RequireHttpsAttribute"/>.
        /// </summary>
        /// <param name="withPermanentRedirect">Tests whether a permanent redirect should be used instead of a temporary one.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder RequiringHttps(bool? withPermanentRedirect = null);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute"/>.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder AllowingAnonymousRequests();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.FormatFilterAttribute"/>.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder AddingFormat();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/>.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder CachingResponse();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="withDuration">Expected duration.</param>
        /// <returns></returns>
        TAttributesTestBuilder CachingResponse(int withDuration);

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="withCacheProfileName">Expected cache profile name.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder CachingResponse(string withCacheProfileName);
        
        /// <summary>
        /// Tests whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="responseCacheAttributeBuilder">Expected <see cref="Microsoft.AspNetCore.Mvc.ResponseCacheAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder CachingResponse(Action<IResponseCacheAttributeTestBuilder> responseCacheAttributeBuilder);
    }
}
