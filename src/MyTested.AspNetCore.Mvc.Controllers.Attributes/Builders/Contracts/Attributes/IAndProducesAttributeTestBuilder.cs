namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> tests.
    /// </summary>
    public interface IAndProducesAttributeTestBuilder : IProducesAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IProducesAttributeTestBuilder"/>.</returns>
        IProducesAttributeTestBuilder AndAlso();
    }
}
