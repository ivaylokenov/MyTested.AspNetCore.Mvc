namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="System.Security.Claims.ClaimsIdentity"/> builder.
    /// </summary>
    public interface IAndClaimsIdentityBuilder : IClaimsIdentityBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building mocked <see cref="System.Security.Claims.ClaimsIdentity"/>.
        /// </summary>
        /// <returns>The same <see cref="IClaimsIdentityBuilder"/>.</returns>
        IClaimsIdentityBuilder AndAlso();
    }
}
