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
    public class WithClaimsPrincipalBuilder : BaseClaimsPrincipalUserBuilder, IAndWithClaimsPrincipalBuilder
    {
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

            base.AddIdentity(claimsIdentity);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsPrincipalBuilder WithIdentity(Action<IWithClaimsIdentityBuilder> claimsIdentityBuilder)
        {
            var newClaimsIdentityBuilder = new WithClaimsIdentityBuilder();
            claimsIdentityBuilder(newClaimsIdentityBuilder);

            base.AddIdentity(newClaimsIdentityBuilder.GetClaimsIdentity());
            return this;
        }

        /// <inheritdoc />
        public IWithClaimsPrincipalBuilder AndAlso() => this;
    }
}
