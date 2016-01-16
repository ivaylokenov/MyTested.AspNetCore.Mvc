namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the the physical file response tests.
    /// </summary>
    public interface IAndPhysicalFileTestBuilder : IPhysicalFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining physical file result tests.
        /// </summary>
        /// <returns>Physical file result test builder.</returns>
        IPhysicalFileTestBuilder AndAlso();
    }
}
