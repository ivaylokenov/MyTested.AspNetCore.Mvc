namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.
    /// </summary>
    public interface IAuthorizeAttributeTestBuilder
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.Policy"/> value as the provided one.
        /// </summary>
        /// <param name="policy">Expected policy.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithPolicy(string policy);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// contains the provided role in its <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.Roles"/> values.
        /// </summary>
        /// <param name="role">Expected role.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithRole(string role);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// contains the provided roles in its <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.Roles"/> values.
        /// </summary>
        /// <param name="roles">Expected roles.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithRoles(IEnumerable<string> roles);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// contains the provided roles in its <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.Roles"/> values.
        /// </summary>
        /// <param name="roles">Expected roles.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithRoles(params string[] roles);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.AuthenticationSchemes"/> value as the provided one.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithAuthenticationSchemes(string authenticationSchemes);
    }
}
