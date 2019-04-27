namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="PhysicalFileResult"/>.
    /// </summary>
    public interface IPhysicalFileTestBuilder 
        : IBaseTestBuilderWithFileResult<IAndPhysicalFileTestBuilder>,
        IBaseTestBuilderWithActionResult<PhysicalFileResult>
    {
    }
}
