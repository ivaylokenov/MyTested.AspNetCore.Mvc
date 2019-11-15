namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute" /> tests.
    /// </summary>
    public interface IAndProducesResponseTypeAttributeTestBuilder : IProducesResponseTypeAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute" />
        /// </summary>
        /// <returns>The same <see cref="IProducesResponseTypeAttributeTestBuilder"/>.</returns>
        IProducesResponseTypeAttributeTestBuilder AndAlso();
    }
}
