namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the action attributes tests.
    /// </summary>
    public interface IAndActionAttributesTestBuilder : IActionAttributesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining action attributes tests.
        /// </summary>
        /// <returns>The same <see cref="IActionAttributesTestBuilder"/>.</returns>
        IActionAttributesTestBuilder AndAlso();
    }
}
