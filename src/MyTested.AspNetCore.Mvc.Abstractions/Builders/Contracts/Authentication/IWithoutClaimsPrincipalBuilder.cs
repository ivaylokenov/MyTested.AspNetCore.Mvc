namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    using System.Security.Claims;

    /// <summary>
    /// Used for building mocked <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public interface IWithoutClaimsPrincipalBuilder
    {
        /// <summary>
        /// Remove Claim with the provided role.
        /// </summary>
        /// <param name="role">The claim's role.</param>
        /// <returns>The same <see cref="IAndWithoutClaimsPrincipalBuilder"/>.</returns>
        IAndWithoutClaimsPrincipalBuilder WithoutRole(string role);

        /// <summary>
        /// Remove Claim with the provided username.
        /// </summary>
        /// <param name="username">The claim's username.</param>
        /// <returns>The same <see cref="IAndWithoutClaimsPrincipalBuilder"/>.</returns>
        IAndWithoutClaimsPrincipalBuilder WithoutUsername(string username);

        /// <summary>
        /// Remove Claim with the provided type and value.
        /// </summary>
        /// <param name="type">The claim's type.</param>
        /// <param name="value">The claim's value.</param>
        /// <returns>The same <see cref="IAndWithoutClaimsPrincipalBuilder"/>.</returns>
        IAndWithoutClaimsPrincipalBuilder WithoutClaim(string type, string value);

        /// <summary>
        /// Removes the provided claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>The same <see cref="IAndWithoutClaimsPrincipalBuilder"/>.</returns>
        IAndWithoutClaimsPrincipalBuilder WithoutClaim(Claim claim);
    }
}
