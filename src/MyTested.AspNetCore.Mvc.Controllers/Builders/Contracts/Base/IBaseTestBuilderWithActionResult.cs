namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<TActionResult> : IBaseTestBuilderWithInvokedAction
    {
    }
}
