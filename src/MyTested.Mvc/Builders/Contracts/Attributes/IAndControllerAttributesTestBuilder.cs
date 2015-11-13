namespace MyTested.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the the attribute tests.
    /// </summary>
    public interface IAndControllerAttributesTestBuilder : IControllerAttributesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IControllerAttributesTestBuilder AndAlso();
    }
}
