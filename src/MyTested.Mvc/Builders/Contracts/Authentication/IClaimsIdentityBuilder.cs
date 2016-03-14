namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IClaimsIdentityBuilder
    {
        /// <summary>
        /// Used for setting ID to the claims identity. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Used for setting username to the claims identity. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithUsername(string username);

        /// <summary>
        /// Used for adding claim to the claims identity.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithClaim(Claim claim);

        /// <summary>
        /// Used for adding claims to the claims identity.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Used for adding claims to the claims identity.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithClaims(params Claim[] claims);

        /// <summary>
        /// Used for setting authentication type to the claims identity. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Used for adding role to the claims identity.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder InRole(string role);

        /// <summary>
        /// Used for adding multiple roles to the claims identity.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Used for adding multiple roles to the claims identity.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same claims identity builder.</returns>
        IAndClaimsIdentityBuilder InRoles(params string[] roles);
    }
}
