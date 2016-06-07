namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<TActionResult> : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithActionResult{TActionResult}"/> type.</returns>
        new IShouldPassForTestBuilderWithActionResult<TActionResult> ShouldPassFor();
    }
}
