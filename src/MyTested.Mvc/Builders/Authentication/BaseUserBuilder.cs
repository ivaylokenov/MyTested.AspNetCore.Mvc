namespace MyTested.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Utilities.Extensions;

    public abstract class BaseUserBuilder
    {
        protected const string DefaultIdentifier = "TestId";
        protected const string DefaultUsername = "TestUser";
        protected const string DefaultAuthenticationType = "Passport";
        protected const string DefaultNameType = ClaimTypes.Name;
        protected const string DefaultRoleType = ClaimTypes.Role;

        private readonly ICollection<Claim> claims;

        private string authenticationType;
        private string nameType;
        private string roleType;

        protected BaseUserBuilder()
        {
            this.authenticationType = DefaultAuthenticationType;
            this.nameType = DefaultNameType;
            this.roleType = DefaultRoleType;

            this.claims = new List<Claim>();
        }
        
        protected static ClaimsIdentity CreateAuthenticatedClaimsIdentity(
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

        protected ClaimsIdentity GetAuthenticatedClaimsIdentity()
        {
            return CreateAuthenticatedClaimsIdentity(
                this.claims,
                this.authenticationType,
                this.nameType,
                this.roleType);
        }

        protected void AddNameType(string nameType)
        {
            this.nameType = nameType;
        }

        protected void AddRoleType(string roleType)
        {
            this.roleType = roleType;
        }

        protected void AddIdentifier(string identifier)
        {
            this.AddClaim(ClaimTypes.NameIdentifier, identifier);
        }

        protected void AddUsername(string username)
        {
            this.AddClaim(this.nameType, username);
        }

        protected void AddClaim(Claim claim)
        {
            this.claims.Add(claim);
        }

        protected void AddClaims(IEnumerable<Claim> claims)
        {
            claims.ForEach(c => this.AddClaim(c));
        }

        protected void AddAuthenticationType(string authenticationType)
        {
            this.authenticationType = authenticationType;
        }

        protected void AddRole(string role)
        {
            this.AddClaim(this.roleType, role);
        }

        protected void AddRoles(IEnumerable<string> roles)
        {
            roles.ForEach(r => this.AddRole(r));
        }

        private void AddClaim(string type, string value)
        {
            this.claims.Add(new Claim(type, value));
        }
    }
}
