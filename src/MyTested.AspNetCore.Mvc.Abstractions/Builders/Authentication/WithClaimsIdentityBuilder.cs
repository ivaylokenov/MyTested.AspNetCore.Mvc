namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Contracts.Authentication;

    /// <summary>
    /// Used for creating mocked authenticated <see cref="ClaimsIdentity"/>.
    /// </summary>
    public class WithClaimsIdentityBuilder : BaseUserBuilder, IAndWithClaimsIdentityBuilder
    {
        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithNameType(string nameType)
        {
            this.AddNameType(nameType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithRoleType(string roleType)
        {
            this.AddRoleType(roleType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithIdentifier(string identifier)
        {
            this.AddIdentifier(identifier);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithUsername(string username)
        {
            this.AddUsername(username);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithClaim(string type, string value) 
            => this.WithClaim(new Claim(type, value));

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithClaim(Claim claim)
        {
            this.AddClaim(claim);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithClaims(IEnumerable<Claim> claims)
        {
            this.AddClaims(claims);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithClaims(params Claim[] claims) 
            => this.WithClaims(claims.AsEnumerable());

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder WithAuthenticationType(string authenticationType)
        {
            this.AddAuthenticationType(authenticationType);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder InRole(string role)
        {
            this.AddRole(role);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder InRoles(IEnumerable<string> roles)
        {
            this.AddRoles(roles);
            return this;
        }

        /// <inheritdoc />
        public IAndWithClaimsIdentityBuilder InRoles(params string[] roles) 
            => this.InRoles(roles.AsEnumerable());

        /// <inheritdoc />
        public IWithClaimsIdentityBuilder AndAlso() => this;

        internal ClaimsIdentity GetClaimsIdentity() 
            => this.GetAuthenticatedClaimsIdentity();
    }
}
