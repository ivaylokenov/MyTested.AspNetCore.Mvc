namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the claims identity builder.
    /// </summary>
    public interface IAndClaimsIdentityBuilder : IClaimsIdentityBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building claims identity.
        /// </summary>
        /// <returns>The same claims identity builder.</returns>
        IClaimsIdentityBuilder AndAlso();
    }
}
