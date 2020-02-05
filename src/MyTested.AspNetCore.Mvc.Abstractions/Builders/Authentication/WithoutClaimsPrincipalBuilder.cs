namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Security.Claims;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;

    /// <summary>
    /// Used for building mocked <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public class WithoutClaimsPrincipalBuilder : BaseClaimsPrincipalUserBuilder, IAndWithoutClaimsPrincipalBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutClaimsPrincipalBuilder"/> class.
        /// </summary>
        public WithoutClaimsPrincipalBuilder(ClaimsPrincipal principal)
            => base.AddClaims(principal.Claims);

        /// <inheritdoc />
        public IAndWithoutClaimsPrincipalBuilder WithoutClaim(string type, string value)
        {
            base.RemoveClaim(type, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutClaimsPrincipalBuilder WithoutClaim(Claim claim)
        {
            base.RemoveClaim(claim);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutClaimsPrincipalBuilder WithoutRole(string role)
        {
            base.RemoveRole(role);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutClaimsPrincipalBuilder WithoutUsername(string username)
        {
            base.RemoveUsername(username);
            return this;
        }

        /// <inheritdoc />
        public IWithoutClaimsPrincipalBuilder AndAlso()
            => this;
    }
}
