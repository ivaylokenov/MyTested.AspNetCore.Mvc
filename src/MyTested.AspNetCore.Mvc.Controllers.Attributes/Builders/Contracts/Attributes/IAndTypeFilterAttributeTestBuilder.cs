namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/> tests.
    /// </summary>
    public interface IAndTypeFilterAttributeTestBuilder : ITypeFilterAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="ITypeFilterAttributeTestBuilder"/>.</returns>
        ITypeFilterAttributeTestBuilder AndAlso();
    }
}
