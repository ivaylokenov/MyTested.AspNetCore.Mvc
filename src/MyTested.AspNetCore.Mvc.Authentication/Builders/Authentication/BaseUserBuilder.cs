namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for creating mocked authenticated <see cref="ClaimsIdentity"/>.
    /// </summary>
    public abstract class BaseUserBuilder
    {
        /// <summary>
        /// Default identifier (Id) of the built <see cref="ClaimsIdentity"/> - "TestId".
        /// </summary>
        protected const string DefaultIdentifier = "TestId";

        /// <summary>
        /// Default username of the built <see cref="ClaimsIdentity"/> - "TestUser".
        /// </summary>
        protected const string DefaultUsername = "TestUser";

        /// <summary>
        /// Default authentication type of the built <see cref="ClaimsIdentity"/> - "Passport".
        /// </summary>
        protected const string DefaultAuthenticationType = "Passport";

        /// <summary>
        /// Default type of the name claim - <see cref="ClaimTypes.Name"/>.
        /// </summary>
        protected const string DefaultNameType = ClaimTypes.Name;

        /// <summary>
        /// Default type of the role claim - <see cref="ClaimTypes.Role"/>.
        /// </summary>
        protected const string DefaultRoleType = ClaimTypes.Role;

        private readonly ICollection<Claim> claims;

        private string authenticationType;
        private string nameType;
        private string roleType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserBuilder"/> class.
        /// </summary>
        protected BaseUserBuilder()
        {
            this.authenticationType = DefaultAuthenticationType;
            this.nameType = DefaultNameType;
            this.roleType = DefaultRoleType;

            this.claims = new List<Claim>();
        }

        /// <summary>
        /// Creates new authenticated claims identity by using the provided claims and authentication type.
        /// </summary>
        /// <param name="claims">Collection of claims to add to the authenticated identity.</param>
        /// <param name="authenticationType">Authentication type to use for the authenticated identity.</param>
        /// <param name="nameType">Type of the username claim. Default is <see cref="ClaimTypes.Name"/>.</param>
        /// <param name="roleType">Type of the role claim. Default is <see cref="ClaimTypes.Role"/>.</param>
        /// <returns>Mocked <see cref="ClaimsIdentity"/>.</returns>
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

        /// <summary>
        /// Creates new authenticated claims identity by using the accumulated claims and authentication type.
        /// </summary>
        /// <returns>Mocked <see cref="ClaimsIdentity"/>.</returns>
        protected ClaimsIdentity GetAuthenticatedClaimsIdentity()
        {
            return CreateAuthenticatedClaimsIdentity(
                this.claims,
                this.authenticationType,
                this.nameType,
                this.roleType);
        }

        /// <summary>
        /// Sets type of the username claim. Default is <see cref="ClaimTypes.Name"/>.
        /// </summary>
        /// <param name="nameType">Type to set on the username claim.</param>
        protected void AddNameType(string nameType)
        {
            this.nameType = nameType;
        }

        /// <summary>
        /// Sets type of the role claim. Default is <see cref="ClaimTypes.Role"/>.
        /// </summary>
        /// <param name="roleType">Type to set on the role claim.</param>
        protected void AddRoleType(string roleType)
        {
            this.roleType = roleType;
        }

        /// <summary>
        /// Sets identifier claim to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="identifier">Value of the identifier claim - <see cref="ClaimTypes.NameIdentifier"/>.</param>
        protected void AddIdentifier(string identifier)
        {
            this.AddClaim(ClaimTypes.NameIdentifier, identifier);
        }

        /// <summary>
        /// Sets username claims to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="ClaimTypes.Name"/>.</param>
        protected void AddUsername(string username)
        {
            this.AddClaim(this.nameType, username);
        }

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="claim">The <see cref="Claim"/> to add.</param>
        protected void AddClaim(Claim claim)
        {
            this.claims.Add(claim);
        }

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="claims">Collection of <see cref="Claim"/> to add.</param>
        protected void AddClaims(IEnumerable<Claim> claims)
        {
            claims.ForEach(c => this.AddClaim(c));
        }

        /// <summary>
        /// Adds authentication type to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="authenticationType">Authentication type to add. Default is "Passport".</param>
        protected void AddAuthenticationType(string authenticationType)
        {
            this.authenticationType = authenticationType;
        }

        /// <summary>
        /// Adds role to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="role">Value of the role claim. Default claim type is <see cref="ClaimTypes.Role"/>.</param>
        protected void AddRole(string role)
        {
            this.AddClaim(this.roleType, role);
        }

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        protected void AddRoles(IEnumerable<string> roles)
        {
            roles.ForEach(r => this.AddRole(r));
        }

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="type">Type of the claim to add.</param>
        /// <param name="value">Value of the claim to add.</param>
        private void AddClaim(string type, string value)
        {
            this.claims.Add(new Claim(type, value));
        }
    }
}
