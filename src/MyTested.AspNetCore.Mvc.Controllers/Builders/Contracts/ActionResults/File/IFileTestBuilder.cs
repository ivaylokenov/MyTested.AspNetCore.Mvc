namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>, <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>.
    /// </summary>
    public interface IFileTestBuilder : IBaseTestBuilderWithFileResult<IAndFileTestBuilder>
    {
    }
}
