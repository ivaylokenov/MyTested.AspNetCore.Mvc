namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Contracts.Authentication;

    /// <summary>
    /// Used for creating mocked authenticated <see cref="ClaimsIdentity"/>.
    /// </summary>
    public class ClaimsIdentityBuilder : BaseUserBuilder, IAndClaimsIdentityBuilder
    {
        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithNameType(string nameType)
        {
            this.AddNameType(nameType);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithRoleType(string roleType)
        {
            this.AddRoleType(roleType);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithIdentifier(string identifier)
        {
            this.AddIdentifier(identifier);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithUsername(string username)
        {
            this.AddUsername(username);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithClaim(string type, string value) 
            => this.WithClaim(new Claim(type, value));

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithClaim(Claim claim)
        {
            this.AddClaim(claim);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithClaims(IEnumerable<Claim> claims)
        {
            this.AddClaims(claims);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithClaims(params Claim[] claims) 
            => this.WithClaims(claims.AsEnumerable());

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder WithAuthenticationType(string authenticationType)
        {
            this.AddAuthenticationType(authenticationType);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder InRole(string role)
        {
            this.AddRole(role);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder InRoles(IEnumerable<string> roles)
        {
            this.AddRoles(roles);
            return this;
        }

        /// <inheritdoc />
        public IAndClaimsIdentityBuilder InRoles(params string[] roles) 
            => this.InRoles(roles.AsEnumerable());

        /// <inheritdoc />
        public IClaimsIdentityBuilder AndAlso() => this;

        internal ClaimsIdentity GetClaimsIdentity() 
            => this.GetAuthenticatedClaimsIdentity();
    }
}
