namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the physical file result tests.
    /// </summary>
    public interface IAndPhysicalFileTestBuilder : IPhysicalFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining physical file result tests.
        /// </summary>
        /// <returns>The same <see cref="IPhysicalFileTestBuilder"/>.</returns>
        IPhysicalFileTestBuilder AndAlso();
    }
}
