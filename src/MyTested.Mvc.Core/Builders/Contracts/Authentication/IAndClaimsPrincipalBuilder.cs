namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="System.Security.Claims.ClaimsPrincipal"/> builder.
    /// </summary>
    public interface IAndClaimsPrincipalBuilder : IClaimsPrincipalBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="System.Security.Claims.ClaimsPrincipal"/>.
        /// </summary>
        /// <returns>The same <see cref="IClaimsPrincipalBuilder"/>.</returns>
        IClaimsPrincipalBuilder AndAlso();
    }
}
