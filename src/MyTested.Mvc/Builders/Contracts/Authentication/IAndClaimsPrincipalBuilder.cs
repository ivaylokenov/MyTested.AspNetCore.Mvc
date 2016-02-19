namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the claims principal builder.
    /// </summary>
    public interface IAndClaimsPrincipalBuilder : IClaimsPrincipalBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building claims principal.
        /// </summary>
        /// <returns>The same claims principal builder.</returns>
        IClaimsPrincipalBuilder AndAlso();
    }
}
