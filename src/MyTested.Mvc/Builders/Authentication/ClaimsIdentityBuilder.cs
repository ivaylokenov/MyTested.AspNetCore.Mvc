namespace MyTested.Mvc.Builders.Authentication
{
    using MyTested.Mvc.Builders.Contracts.Authentication;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public class ClaimsIdentityBuilder : BaseUserBuilder, IAndClaimsIdentityBuilder
    {
        public IAndClaimsIdentityBuilder WithNameType(string nameType)
        {
            this.AddNameType(nameType);
            return this;
        }

        public IAndClaimsIdentityBuilder WithRoleType(string roleType)
        {
            this.AddRoleType(roleType);
            return this;
        }

        /// <summary>
        /// Used for setting ID to the claims identity. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithIdentifier(string identifier)
        {
            this.AddIdentifier(identifier);
            return this;
        }

        /// <summary>
        /// Used for setting username to the claims identity. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithUsername(string username)
        {
            this.AddUsername(username);
            return this;
        }

        /// <summary>
        /// Used for adding claim to the claims identity.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithClaim(Claim claim)
        {
            this.AddClaim(claim);
            return this;
        }

        /// <summary>
        /// Used for adding claims to the claims identity.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithClaims(IEnumerable<Claim> claims)
        {
            this.AddClaims(claims);
            return this;
        }

        /// <summary>
        /// Used for adding claims to the claims identity.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithClaims(params Claim[] claims)
        {
            return this.WithClaims(claims.AsEnumerable());
        }

        /// <summary>
        /// Used for setting authentication type to the claims identity. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder WithAuthenticationType(string authenticationType)
        {
            this.AddAuthenticationType(authenticationType);
            return this;
        }

        /// <summary>
        /// Used for adding role to claims identity.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder InRole(string role)
        {
            this.AddRole(role);
            return this;
        }

        /// <summary>
        /// Used for adding multiple roles to claims identity.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder InRoles(IEnumerable<string> roles)
        {
            this.AddRoles(roles);
            return this;
        }

        /// <summary>
        /// Used for adding multiple roles to claims identity.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same claims identity builder.</returns>
        public IAndClaimsIdentityBuilder InRoles(params string[] roles)
        {
            return this.InRoles(roles.AsEnumerable());
        }

        public IClaimsIdentityBuilder AndAlso()
        {
            return this;
        }

        internal ClaimsIdentity GetClaimsIdentity()
        {
            return this.GetAuthenticatedClaimsIdentity();
        }
    }
}
