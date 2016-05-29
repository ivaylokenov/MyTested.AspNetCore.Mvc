namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>, <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> tests.
    /// </summary>
    public interface IAndFileTestBuilder : IFileTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>, <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IFileTestBuilder"/>.</returns>
        IFileTestBuilder AndAlso();
    }
}
