namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute"/> tests.
    /// </summary>
    public interface IAndServiceFilterAttributeTestBuilder : IServiceFilterAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IServiceFilterAttributeTestBuilder"/>.</returns>
        IServiceFilterAttributeTestBuilder AndAlso();
    }
}
