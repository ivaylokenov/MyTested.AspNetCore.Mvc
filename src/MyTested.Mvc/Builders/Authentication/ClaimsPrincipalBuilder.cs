namespace MyTested.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Contracts.Authentication;
    using Utilities.Extensions;
    using System.Security.Principal;

    /// <summary>
    /// Used for building mocked claims principal.
    /// </summary>
    public class ClaimsPrincipalBuilder : IAndClaimsPrincipalBuilder
    {
        private const string DefaultIdentifier = "TestId";
        private const string DefaultUsername = "TestUser";
        private const string DefaultAuthenticationType = "Passport";
        private const string DefaultNameType = ClaimTypes.Name;
        private const string DefaultRoleType = ClaimTypes.Role;

        private ICollection<Claim> claims;
        private ICollection<ClaimsIdentity> identities;

        private string authenticationType;
        private string nameType;
        private string roleType;
        
        public ClaimsPrincipalBuilder()
        {
            this.authenticationType = DefaultAuthenticationType;
            this.nameType = DefaultNameType;
            this.roleType = DefaultRoleType;

            this.claims = new List<Claim>();
            this.identities = new List<ClaimsIdentity>();
        }

        /// <summary>
        /// Static constructor for creating default authenticated claims principal with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>Authenticated claims principal.</returns>
        public static ClaimsPrincipal CreateDefaultAuthenticated()
        {
            return new ClaimsPrincipal(GetAuthenticatedClaimsIdentity());
        }

        public IAndClaimsPrincipalBuilder WithNameType(string nameType)
        {
            this.nameType = nameType;
            return this;
        }

        public IAndClaimsPrincipalBuilder WithRoleType(string roleType)
        {
            this.roleType = roleType;
            return this;
        }

        /// <summary>
        /// Used for setting ID to the claims principal. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithIdentifier(string identifier)
        {
            this.claims.Add(new Claim(ClaimTypes.NameIdentifier, identifier));
            return this;
        }

        /// <summary>
        /// Used for setting username to the claims principal. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithUsername(string username)
        {
            this.claims.Add(new Claim(this.nameType, username));
            return this;
        }

        /// <summary>
        /// Used for adding claim to the claims principal.
        /// </summary>
        /// <param name="claim">The claim to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithClaim(Claim claim)
        {
            this.claims.Add(claim);
            return this;
        }

        /// <summary>
        /// Used for adding claims to the claims principal.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims)
        {
            claims.ForEach(claim => this.claims.Add(claim));
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
            this.authenticationType = authenticationType;
            return this;
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

        public IAndClaimsPrincipalBuilder WithIdentity()
        {
            return this;
        }

        /// <summary>
        /// Used for adding role to claims principal.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder InRole(string role)
        {
            this.claims.Add(new Claim(roleType, role));
            return this;
        }

        /// <summary>
        /// Used for adding multiple roles to claims principal.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same claims principal builder.</returns>
        public IAndClaimsPrincipalBuilder InRoles(IEnumerable<string> roles)
        {
            roles.ForEach(role => this.InRole(role));
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
            var claimsPrincipal = new ClaimsPrincipal(GetAuthenticatedClaimsIdentity(
                this.claims,
                this.authenticationType,
                this.nameType,
                this.roleType));

            claimsPrincipal.AddIdentities(this.identities.AsEnumerable());

            return claimsPrincipal;
        }
        
        private static ClaimsIdentity GetAuthenticatedClaimsIdentity(
            ICollection<Claim> claims = null,
            string authenticationType = null,
            string nameType = null,
            string roleType = null)
        {
            claims = claims ?? new List<Claim>();
            authenticationType = authenticationType ?? DefaultAuthenticationType;
            nameType = nameType ?? DefaultNameType;
            roleType = roleType ?? DefaultRoleType;

            if (claims.All(c => c.Type != ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, DefaultIdentifier));
            }

            if (claims.All(c => c.Type != nameType))
            {
                claims.Add(new Claim(nameType, DefaultUsername));
            }

            return new ClaimsIdentity(claims, authenticationType, nameType, roleType);
        }
    }
}
