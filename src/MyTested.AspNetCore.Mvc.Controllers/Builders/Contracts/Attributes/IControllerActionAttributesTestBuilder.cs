namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Base interface for all controller action attribute test builders.
    /// </summary>
    /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type for common methods.</typeparam>
    public interface IControllerActionAttributesTestBuilder<TAttributesTestBuilder> : IBaseAttributesTestBuilder<TAttributesTestBuilder>
        where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Checks whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
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
        /// Checks whether the collected attributes contain <see cref="Microsoft.AspNetCore.Mvc.AreaAttribute"/>.
        /// </summary>
        /// <param name="areaName">Expected area name.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder SpecifyingArea(string areaName);

        /// <summary>
        /// Checks whether the collected attributes contain <see cref="Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute"/>.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder AllowingAnonymousRequests();

        /// <summary>
        /// Checks whether the collected attributes contain <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null);
    }
}
