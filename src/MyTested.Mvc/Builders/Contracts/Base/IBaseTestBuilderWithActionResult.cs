namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<TActionResult> : IBaseTestBuilderWithInvokedAction
    {
        new IShouldPassForTestBuilderWithActionResult<TActionResult> ShouldPassFor();
    }
}
