namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.
    /// </summary>
    public interface IAuthorizeAttributeTestBuilder
    {
        /// <summary>
        /// Tests whether an <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.Policy"/> value as the provided one.
        /// </summary>
        /// <param name="policy">Expected policy.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithPolicy(string policy);

        /// <summary>
        /// Tests whether an <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute.AuthenticationSchemes"/> value as the provided one.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>The same <see cref="IAndAuthorizeAttributeTestBuilder"/>.</returns>
        IAndAuthorizeAttributeTestBuilder WithAuthenticationSchemes(string authenticationSchemes);
    }
}
