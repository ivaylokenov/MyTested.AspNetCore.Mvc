namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using Contracts.Authentication;

    /// <summary>
    /// Used for building mocked <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public class WithClaimsPrincipalBuilder : BaseUserBuilder, IAndWithClaimsPrincipalBuilder
    {
        private readonly ICollection<ClaimsIdentity> identities;

        /// <summary>
        /// Initializes a new instance of the <see cref="WithClaimsPrincipalBuilder"/> class.
        /// </summary>
        public WithClaimsPrincipalBuilder()
            => this.identities = new List<ClaimsIdentity>();

        /// <summary>
        /// Static constructor for creating default authenticated claims principal with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>Authenticated <see cref="ClaimsPrincipal"/>.</returns>
        /// <value>Result of type <see cref="ClaimsPrincipal"/>.</value>
        public static ClaimsPrincipal DefaultAuthenticated { get; } 
            = new ClaimsPrincipal(CreateAuthenticatedClaimsIdentity());

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithNameType(string nameType)
        {
            this.AddNameType(nameType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithRoleType(string roleType)
        {
            this.AddRoleType(roleType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithIdentifier(string identifier)
        {
            this.AddIdentifier(identifier);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithUsername(string username)
        {
            this.AddUsername(username);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithClaim(string type, string value) 
            => this.WithClaim(new Claim(type, value));

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithClaim(Claim claim)
        {
            this.AddClaim(claim);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims)
        {
            this.AddClaims(claims);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithClaims(params Claim[] claims) 
            => this.WithClaims(claims.AsEnumerable());

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithAuthenticationType(string authenticationType)
        {
            this.AddAuthenticationType(authenticationType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder InRole(string role)
        {
            this.AddRole(role);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder InRoles(IEnumerable<string> roles)
        {
            this.AddRoles(roles);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder InRoles(params string[] roles) 
            => this.InRoles(roles.AsEnumerable());

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithIdentity(IIdentity identity)
        {
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                claimsIdentity = new ClaimsIdentity(identity);
            }

            this.identities.Add(claimsIdentity);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithIdentity(Action<IWithClaimsIdentityBuilder> claimsIdentityBuilder)
        {
            var newClaimsIdentityBuilder = new WithClaimsIdentityBuilder();
            claimsIdentityBuilder(newClaimsIdentityBuilder);
            this.identities.Add(newClaimsIdentityBuilder.GetClaimsIdentity());
            return this;
        }

        /// <inheritdoc />
        public IWithClaimsPrincipalBuilder AndAlso() => this;

        public ClaimsPrincipal GetClaimsPrincipal()
        {
            var claimIdentities = this.identities.Reverse().ToList();
            claimIdentities.Add(this.GetAuthenticatedClaimsIdentity());

            var claimsPrincipal = new ClaimsPrincipal(claimIdentities);
            
            return claimsPrincipal;
        }
    }
}
