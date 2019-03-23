namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
    /// </summary>
    public interface IPhysicalFileTestBuilder : IBaseFileTestBuilder<IAndPhysicalFileTestBuilder>
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// has the same physical file path as the provided one.
        /// </summary>
        /// <param name="physicalPath">File physical path as string.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        IAndPhysicalFileTestBuilder WithPath(string physicalPath);
    }
}
