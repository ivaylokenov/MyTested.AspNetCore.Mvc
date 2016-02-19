namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// Used for building claims principal.
    /// </summary>
    public interface IClaimsPrincipalBuilder
    {
        /// <summary>
        /// Used for setting ID to the claims principal. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Used for setting username to the claims principal. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithUsername(string username);

        /// <summary>
        /// Used for adding claim to the claims principal.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithClaim(Claim claim);

        /// <summary>
        /// Used for adding claims to the claims principal.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Used for adding claims to the claims principal.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithClaims(params Claim[] claims);

        /// <summary>
        /// Used for setting authentication type to the claims principal. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Used for adding role to the claims principal.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder InRole(string role);

        /// <summary>
        /// Used for adding multiple roles to the claims principal.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Used for adding multiple roles to the claims principal.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same claims principal builder.</returns>
        IAndClaimsPrincipalBuilder InRoles(params string[] roles);
    }
}
