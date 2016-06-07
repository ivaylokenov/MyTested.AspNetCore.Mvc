namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the physical <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/> tests.
    /// </summary>
    public interface IAndPhysicalFileTestBuilder : IPhysicalFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IPhysicalFileTestBuilder"/>.</returns>
        IPhysicalFileTestBuilder AndAlso();
    }
}
