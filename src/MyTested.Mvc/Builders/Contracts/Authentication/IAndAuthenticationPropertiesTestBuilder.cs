namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the authentication properties tests.
    /// </summary>
    public interface IAndAuthenticationPropertiesTestBuilder : IAuthenticationPropertiesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing authentication properties.
        /// </summary>
        /// <returns>The same authentication properties test builder.</returns>
        IAuthenticationPropertiesTestBuilder AndAlso();
    }
}
