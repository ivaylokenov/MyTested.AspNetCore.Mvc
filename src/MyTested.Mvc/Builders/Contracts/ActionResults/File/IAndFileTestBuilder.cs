namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the file result tests.
    /// </summary>
    public interface IAndFileTestBuilder : IFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining file result tests.
        /// </summary>
        /// <returns>The same <see cref="IFileTestBuilder"/>.</returns>
        IFileTestBuilder AndAlso();
    }
}
