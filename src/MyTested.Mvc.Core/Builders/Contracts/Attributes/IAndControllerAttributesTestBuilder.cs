namespace MyTested.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the controller attributes tests.
    /// </summary>
    public interface IAndControllerAttributesTestBuilder : IControllerAttributesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining controller attributes tests.
        /// </summary>
        /// <returns>The same <see cref="IControllerAttributesTestBuilder"/>.</returns>
        IControllerAttributesTestBuilder AndAlso();
    }
}
