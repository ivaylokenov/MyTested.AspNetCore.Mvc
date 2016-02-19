namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    public interface IClaimsIdentityBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building claims identity.
        /// </summary>
        /// <returns>The same claims identity builder.</returns>
        IClaimsPrincipalBuilder AndAlso();
    }
}
