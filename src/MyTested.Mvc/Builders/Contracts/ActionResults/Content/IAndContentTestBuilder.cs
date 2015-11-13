namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    /// <summary>
    /// Used for adding AndAlso() method to the the content response tests.
    /// </summary>
    public interface IAndContentTestBuilder : IContentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        IContentTestBuilder AndAlso();
    }
}
