namespace MyTested.Mvc.Builders.Contracts.ActionResults.Created
{
    /// <summary>
    /// Used for adding AndAlso() method to the the created response tests.
    /// </summary>
    public interface IAndCreatedTestBuilder : ICreatedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        ICreatedTestBuilder AndAlso();
    }
}
