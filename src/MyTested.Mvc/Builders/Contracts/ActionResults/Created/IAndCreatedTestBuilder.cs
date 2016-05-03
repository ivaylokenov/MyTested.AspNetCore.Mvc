namespace MyTested.Mvc.Builders.Contracts.ActionResults.Created
{
    /// <summary>
    /// Used for adding AndAlso() method to the created result tests.
    /// </summary>
    public interface IAndCreatedTestBuilder : ICreatedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same <see cref="ICreatedTestBuilder"/>.</returns>
        ICreatedTestBuilder AndAlso();
    }
}
