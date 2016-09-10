namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    /// <summary>
    /// Contains AndAlso() method allowing additional assertions after the view component result tests.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IAndViewComponentResultTestBuilder<TInvocationResult> 
        : IViewComponentShouldHaveTestBuilder<TInvocationResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the view component tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentResultTestBuilder{TInvocationResult}"/>.</returns>
        IViewComponentResultTestBuilder<TInvocationResult> AndAlso();
    }
}
