namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Base class for creating mocked authenticated <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public class BaseClaimsPrincipalUserBuilder : BaseUserBuilder
    {
        private readonly ICollection<ClaimsIdentity> identities;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClaimsPrincipalUserBuilder"/> class.
        /// </summary>
        public BaseClaimsPrincipalUserBuilder()
            => this.identities = new List<ClaimsIdentity>();

        /// <summary>
        /// Returns the principle based on provided identities and claims.
        /// </summary>
        /// <returns>This <see cref="ClaimsPrincipal"/>.</returns>
        public ClaimsPrincipal GetClaimsPrincipal()
        {
            var claimIdentities = this.identities.Reverse().ToList();
            claimIdentities.Add(this.GetAuthenticatedClaimsIdentity());

            var claimsPrincipal = new ClaimsPrincipal(claimIdentities);

            return claimsPrincipal;
        }

        /// <summary>
        /// Returns the principle based on provided claims only.
        /// </summary>
        /// <returns>This <see cref="ClaimsPrincipal"/>.</returns>
        public ClaimsPrincipal GetClaimsPrincipalBasedOnClaimsOnly()
        {
            var claimsPrincipal = new ClaimsPrincipal(this.GetAuthenticatedClaimsIdentity());

            return claimsPrincipal;
        }

        /// <summary>
        /// Static constructor for creating default authenticated claims principal with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>Authenticated <see cref="ClaimsPrincipal"/>.</returns>
        /// <value>Result of type <see cref="ClaimsPrincipal"/>.</value>
        public static ClaimsPrincipal DefaultAuthenticated { get; }
            = new ClaimsPrincipal(CreateAuthenticatedClaimsIdentity());

        protected void AddIdentity(ClaimsIdentity identity)
            => this.identities.Add(identity);   

        protected void AddIdentities(IEnumerable<ClaimsIdentity> identities)
        {
            foreach (var identity in identities)
            {
                this.AddIdentity(identity);
            }
        }
    }
}
