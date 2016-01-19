namespace MyTested.Mvc.Internal.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Mocked ClaimsPrinciple object.
    /// </summary>
    public class MockedClaimsPrincipal : ClaimsPrincipal
    {
        private const string DefaultUsername = "TestUser";
        private const string DefaultIPrincipalType = "Passport";

        private readonly IEnumerable<string> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedClaimsPrincipal" /> class.
        /// </summary>
        /// <param name="username">Initial username.</param>
        /// <param name="principalType">Initial principal type.</param>
        /// <param name="roles">Initial user roles.</param>
        public MockedClaimsPrincipal(
            string username = null,
            string principalType = null,
            IEnumerable<string> roles = null)
            : base(new MockedIIdentity(
                username ?? DefaultUsername,
                principalType ?? DefaultIPrincipalType,
                true))
        {
            this.roles = roles ?? new HashSet<string>();
        }

        /// <summary>
        /// Gets the IIdentity of the IPrinciple.
        /// </summary>
        /// <value>IIdentity object.</value>
        public new IIdentity Identity { get; private set; }
        
        /// <summary>
        /// Static constructor for creating default authenticated mocked user object with "TestUser" username.
        /// </summary>
        /// <returns>Authenticated IPrincipal.</returns>
        public static ClaimsPrincipal CreateDefaultAuthenticated()
        {
            return new ClaimsPrincipal(new MockedIIdentity(DefaultUsername, DefaultIPrincipalType, true));
        }

        /// <summary>
        /// Checks whether the current user is in user role.
        /// </summary>
        /// <param name="role">User role to check.</param>
        /// <returns>True or False.</returns>
        public override bool IsInRole(string role)
        {
            return this.roles.Contains(role);
        }
    }
}
