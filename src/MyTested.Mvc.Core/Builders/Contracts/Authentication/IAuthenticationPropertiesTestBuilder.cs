namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>.
    /// </summary>
    public interface IAuthenticationPropertiesTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// has the same <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.AllowRefresh"/> value as the provided one.
        /// </summary>
        /// <param name="allowRefresh">Expected allow refresh value.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithAllowRefresh(bool? allowRefresh);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// has the same <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.ExpiresUtc"/> value as the provided one.
        /// </summary>
        /// <param name="expiresUtc">Expected expires value.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithExpires(DateTimeOffset? expiresUtc);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// has the same <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.IsPersistent"/> value as the provided one.
        /// </summary>
        /// <param name="isPersistent">Expected is persistent value.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithIsPersistent(bool isPersistent);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// has the same <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.IssuedUtc"/> value as the provided one.
        /// </summary>
        /// <param name="issuedUtc">Expected issued value.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithIssued(DateTimeOffset? issuedUtc);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// contains the provided item key in its <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.Items"/> collection.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/> contains the provided item key and value
        /// in its <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.Items"/> collection.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <param name="itemValue">Expected item value.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey, string itemValue);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/> contains the provided items
        /// in its <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.Items"/> collection.
        /// </summary>
        /// <param name="items">Expected items as anonymous object.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItems(object items);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/> contains the provided items
        /// in its <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.Items"/> collection.
        /// </summary>
        /// <param name="items">Expected items as dictionary.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithItems(IDictionary<string, string> items);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties"/>
        /// has the same <see cref="Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.RedirectUri"/> as the provided one.
        /// </summary>
        /// <param name="redirectUri">Expected redirect URI.</param>
        /// <returns>The same <see cref="IAndAuthenticationPropertiesTestBuilder"/>.</returns>
        IAndAuthenticationPropertiesTestBuilder WithRedirectUri(string redirectUri);
    }
}
