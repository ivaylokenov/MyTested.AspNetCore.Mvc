namespace MyTested.Mvc.Builders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using Internal.Extensions;
    using Internal.Identity;
    using Contracts;
    using System.Security.Claims;
    /// <summary>
    /// Used for building mocked Controller.User object.
    /// </summary>
    public class UserBuilder : IUserBuilder
    {
        private readonly ICollection<string> constructedRoles;

        private string constructedUsername;
        private string constructedAuthenticationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBuilder" /> class.
        /// </summary>
        public UserBuilder()
        {
            this.constructedRoles = new HashSet<string>();
        }

        /// <summary>
        /// Used for setting username to the mocked user object.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder WithUsername(string username)
        {
            this.constructedUsername = username;
            return this;
        }

        /// <summary>
        /// Used for setting authentication type to the mocked user object.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder WithAuthenticationType(string authenticationType)
        {
            this.constructedAuthenticationType = authenticationType;
            return this;
        }

        /// <summary>
        /// Used for adding user role to the mocked user object.
        /// </summary>
        /// <param name="role">The user role to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRole(string role)
        {
            this.constructedRoles.Add(role);
            return this;
        }

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRoles(IEnumerable<string> roles)
        {
            roles.ForEach(role => this.constructedRoles.Add(role));
            return this;
        }

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRoles(params string[] roles)
        {
            return this.InRoles(roles.AsEnumerable());
        }

        internal ClaimsPrincipal GetUser()
        {
            return new MockedClaimsPrincipal(
                this.constructedUsername,
                this.constructedAuthenticationType,
                this.constructedRoles);
        }
    }
}
