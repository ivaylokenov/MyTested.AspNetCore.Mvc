namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the the file response tests.
    /// </summary>
    public interface IAndFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining file result tests.
        /// </summary>
        /// <returns>File result test builder.</returns>
        IFileTestBuilder AndAlso();
    }
}
