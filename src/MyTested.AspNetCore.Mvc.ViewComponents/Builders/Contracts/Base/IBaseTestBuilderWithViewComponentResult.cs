namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with view component result.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IBaseTestBuilderWithViewComponentResult<TInvocationResult> : IBaseTestBuilderWithViewComponent
    {
    }
}
