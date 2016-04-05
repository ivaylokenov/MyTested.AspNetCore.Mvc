namespace MyTested.Mvc.Builders.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using Contracts.Authentication;

    /// <summary>
    /// Used for building mocked claims principal.
    /// </summary>
    public class ClaimsPrincipalBuilder : BaseUserBuilder, IAndClaimsPrincipalBuilder
    {
        private static readonly ClaimsPrincipal DefaultAuthenticatedClaimsPrinciple = new ClaimsPrincipal(CreateAuthenticatedClaimsIdentity());

        private ICollection<ClaimsIdentity> identities;

        public ClaimsPrincipalBuilder()
        {
            this.identities = new List<ClaimsIdentity>();
        }

        /// <summary>
        /// Static constructor for creating default authenticated claims principal with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>Authenticated claims principal.</returns>
        public static ClaimsPrincipal DefaultAuthenticated => DefaultAuthenticatedClaimsPrinciple;

        public IAndClaimsPrincipalBuilder WithNameType(string nameType)
        {
            this.AddNameType(nameType);
            return this;
        }

        public IAndClaimsPrincipalBuilder WithRoleType(string roleType)
        {
            this.AddRoleType(roleType);
            return this;
        }

        /// <summary>
        /// Used for setting ID to the claims principal. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithIdentifier(string identifier)
        {
            this.AddIdentifier(identifier);
            return this;
        }

        /// <summary>
        /// Used for setting username to the claims principal. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithUsername(string username)
        {
            this.AddUsername(username);
            return this;
        }

        public IAndClaimsPrincipalBuilder WithClaim(string type, string value)
        {
            return this.WithClaim(new Claim(type, value));
        }

        /// <summary>
        /// Used for adding claim to the claims principal.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithClaim(Claim claim)
        {
            this.AddClaim(claim);
            return this;
        }

        /// <summary>
        /// Used for adding claims to the claims principal.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims)
        {
            this.AddClaims(claims);
            return this;
        }

        /// <summary>
        /// Used for adding claims to the claims principal.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithClaims(params Claim[] claims)
        {
            return this.WithClaims(claims.AsEnumerable());
        }

        /// <summary>
        /// Used for setting authentication type to the claims principal. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithAuthenticationType(string authenticationType)
        {
            this.AddAuthenticationType(authenticationType);
            return this;
        }

        /// <summary>
        /// Used for adding role to claims principal.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder InRole(string role)
        {
            this.AddRole(role);
            return this;
        }

        /// <summary>
        /// Used for adding multiple roles to claims principal.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder InRoles(IEnumerable<string> roles)
        {
            this.AddRoles(roles);
            return this;
        }

        /// <summary>
        /// Used for adding multiple roles to claims principal.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder InRoles(params string[] roles)
        {
            return this.InRoles(roles.AsEnumerable());
        }

        public IAndClaimsPrincipalBuilder WithIdentity(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                claimsIdentity = new ClaimsIdentity(identity);
            }

            this.identities.Add(claimsIdentity);
            return this;
        }

        public IAndClaimsPrincipalBuilder WithIdentity(Action<IClaimsIdentityBuilder> claimsIdentityBuilder)
        {
            var newClaimsIdentityBuilder = new ClaimsIdentityBuilder();
            claimsIdentityBuilder(newClaimsIdentityBuilder);
            this.identities.Add(newClaimsIdentityBuilder.GetClaimsIdentity());
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when building claims principal.
        /// </summary>
        /// <returns>The same claims principal builder.</returns>
        public IClaimsPrincipalBuilder AndAlso()
        {
            return this;
        }
        
        internal ClaimsPrincipal GetClaimsPrincipal()
        {
            var identities = this.identities.Reverse().ToList();
            identities.Add(this.GetAuthenticatedClaimsIdentity());

            var claimsPrincipal = new ClaimsPrincipal(identities);
            
            return claimsPrincipal;
        }
    }
}
