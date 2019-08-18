namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Conflict
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ConflictResult"/>
    /// or <see cref="ConflictObjectResult"/> result.
    /// </summary>
    public interface IConflictTestBuilder
        : IBaseTestBuilderWithErrorResult<IAndConflictTestBuilder>,
        IBaseTestBuilderWithActionResult<ConflictObjectResult>
    {
    }
}
