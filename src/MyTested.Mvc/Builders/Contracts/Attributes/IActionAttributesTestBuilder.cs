namespace MyTested.Mvc.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc.Infrastructure;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public interface IActionAttributesTestBuilder
    {
        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;

        /// <summary>
        /// Checks whether the collected attributes contain ActionNameAttribute.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName);

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null);

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder AllowingAnonymousRequests();

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(string withAllowedRoles = null);

        /// <summary>
        /// Checks whether the collected attributes contain NonActionAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder DisablingActionCall();
    }
}
