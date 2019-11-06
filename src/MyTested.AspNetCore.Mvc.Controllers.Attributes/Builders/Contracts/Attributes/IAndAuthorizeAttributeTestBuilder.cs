namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() to the <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute" /> tests.
    /// </summary>
    public interface IAndAuthorizeAttributeTestBuilder : IAuthorizeAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute" />
        /// </summary>
        /// <returns>The same <see cref="IAuthorizeAttributeTestBuilder"/>.</returns>
        IAuthorizeAttributeTestBuilder AndAlso();
    }
}
