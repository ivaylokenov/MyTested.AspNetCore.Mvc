namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.ActionResult
{
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">Type of the expected result.</typeparam>
    public interface IActionResultOfTTestBuilder<TResult>
        : IBaseTestBuilderWithActionResult<TResult>
    {
    }
}
