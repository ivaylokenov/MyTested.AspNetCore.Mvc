namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    /// <summary>
    /// Used for adding AndAlso() method to the content result tests.
    /// </summary>
    public interface IAndContentTestBuilder : IContentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same <see cref="IContentTestBuilder"/>.</returns>
        IContentTestBuilder AndAlso();
    }
}
