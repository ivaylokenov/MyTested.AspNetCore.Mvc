namespace MyTested.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public interface IActionAttributesTestBuilder
    {
        /// <summary>
        /// Tests whether the action attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;

        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Mvc.ActionNameAttribute"/>.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName);

        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null);

        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder AllowingAnonymousRequests();

        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(string withAllowedRoles = null);

        /// <summary>
        /// Tests whether the action attributes contain <see cref="Microsoft.AspNetCore.Mvc.NonActionAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        IAndActionAttributesTestBuilder DisablingActionCall();
    }
}
