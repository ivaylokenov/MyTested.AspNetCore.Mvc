namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/> tests.
    /// </summary>
    public interface IAndRequestFormLimitsAttributeTestBuilder : IRequestFormLimitsAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IRequestFormLimitsAttributeTestBuilder AndAlso();
    }
}
