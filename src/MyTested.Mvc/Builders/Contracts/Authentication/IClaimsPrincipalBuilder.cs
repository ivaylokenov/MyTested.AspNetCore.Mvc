namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Used for building mocked <see cref="System.Security.Claims.ClaimsPrincipal"/>.
    /// </summary>
    public interface IClaimsPrincipalBuilder
    {
        /// <summary>
        /// Sets type of the username claim. Default is <see cref="ClaimTypes.Name"/>.
        /// </summary>
        /// <param name="nameType">Type to set on the username claim.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithNameType(string nameType);

        /// <summary>
        /// Sets type of the role claim. Default is <see cref="ClaimTypes.Role"/>.
        /// </summary>
        /// <param name="roleType">Type to set on the role claim.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithRoleType(string roleType);

        /// <summary>
        /// Sets identifier claim to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">Value of the identifier claim - <see cref="ClaimTypes.NameIdentifier"/>.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Sets username claims to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="ClaimTypes.Name"/>.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithUsername(string username);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="type">Type of the <see cref="Claim"/> to add.</param>
        /// <param name="value">Value of the <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithClaim(string type, string value);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claim">The <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithClaim(Claim claim);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claims">Enumerable of <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claims"><see cref="Claim"/> parameters to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithClaims(params Claim[] claims);

        /// <summary>
        /// Adds authentication type to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">Authentication type to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Adds role to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="role">Value of the role claim. Default claim type is <see cref="ClaimTypes.Role"/>.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder InRole(string role);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="roles">Enumerable of role names to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="roles">Role name parameters to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder InRoles(params string[] roles);

        /// <summary>
        /// Adds <see cref="IIdentity"/> to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="identity"><see cref="IIdentity"/> to add.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithIdentity(IIdentity identity);

        /// <summary>
        /// Adds <see cref="IIdentity"/> to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsIdentityBuilder">Builder for creating mocked <see cref="IIdentity"/>.</param>
        /// <returns>The same <see cref="IAndClaimsPrincipalBuilder"/>.</returns>
        IAndClaimsPrincipalBuilder WithIdentity(Action<IClaimsIdentityBuilder> claimsIdentityBuilder);
    }
}
