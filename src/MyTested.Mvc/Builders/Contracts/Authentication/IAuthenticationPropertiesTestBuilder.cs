namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing authentication properties.
    /// </summary>
    public interface IAuthenticationPropertiesTestBuilder
    {
        /// <summary>
        /// Tests whether authentication properties has the same allow refresh value as the provided one.
        /// </summary>
        /// <param name="allowRefresh">Expected allow refresh value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithAllowRefresh(bool? allowRefresh);

        /// <summary>
        /// Tests whether authentication properties has the same expires value as the provided one.
        /// </summary>
        /// <param name="expiresUtc">Expected expires value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithExpires(DateTimeOffset? expiresUtc);

        /// <summary>
        /// Tests whether authentication properties has the same is persistent value as the provided one.
        /// </summary>
        /// <param name="isPersistent">Expected is persistent value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithIsPersistent(bool isPersistent);

        /// <summary>
        /// Tests whether authentication properties has the same issued value as the provided one.
        /// </summary>
        /// <param name="issuedUtc">Expected issued value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithIssued(DateTimeOffset? issuedUtc);

        /// <summary>
        /// Tests whether authentication properties contains the provided item key.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey);

        /// <summary>
        /// Tests whether authentication properties contains the provided item key and value.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <param name="itemValue">Expected item value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey, string itemValue);

        /// <summary>
        /// Tests whether authentication properties contains the provided items.
        /// </summary>
        /// <param name="items">Expected items as dictionary.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItems(IDictionary<string, string> items);

        /// <summary>
        /// Tests whether authentication properties has the same redirect URI as the provided one.
        /// </summary>
        /// <param name="redirectUri">Expected redirect URI.</param>
        /// <returns>The same authentication properties test builder.</returns>
        IAndAuthenticationPropertiesTestBuilder WithRedirectUri(string redirectUri);
    }
}
