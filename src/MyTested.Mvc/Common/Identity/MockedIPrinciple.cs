namespace MyTested.Mvc.Common.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    /// <summary>
    /// Mocked IPrinciple object.
    /// </summary>
    public class MockedIPrinciple : IPrincipal
    {
        private const string DefaultUsername = "TestUser";
        private const string DefaultIPrincipalType = "Passport";

        private readonly IEnumerable<string> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedIPrinciple" /> class.
        /// </summary>
        /// <param name="username">Initial username.</param>
        /// <param name="principalType">Initial principal type.</param>
        /// <param name="roles">Initial user roles.</param>
        public MockedIPrinciple(
            string username = null,
            string principalType = null,
            IEnumerable<string> roles = null)
        {
            this.roles = roles ?? new HashSet<string>();
            this.Identity = new MockedIIdentity(
                username ?? DefaultUsername,
                principalType ?? DefaultIPrincipalType,
                true);
        }

        /// <summary>
        /// Gets the IIdentity of the IPrinciple.
        /// </summary>
        /// <value>IIdentity object.</value>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Static constructor for creating default unauthenticated mocked user object.
        /// </summary>
        /// <returns>Unauthenticated IPrincipal.</returns>
        public static IPrincipal CreateUnauthenticated()
        {
            return new MockedIPrinciple
            {
                Identity = new MockedIIdentity()
            };
        }

        /// <summary>
        /// Static constructor for creating default authenticated mocked user object with "TestUser" username.
        /// </summary>
        /// <returns>Authenticated IPrincipal.</returns>
        public static IPrincipal CreateDefaultAuthenticated()
        {
            return new MockedIPrinciple()
            {
                Identity = new MockedIIdentity(DefaultUsername, DefaultIPrincipalType, true)
            };
        }

        /// <summary>
        /// Checks whether the current user is in user role.
        /// </summary>
        /// <param name="role">User role to check.</param>
        /// <returns>True or False.</returns>
        public bool IsInRole(string role)
        {
            return this.roles.Contains(role);
        }
    }
}
