namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using Base;
    using CaughtExceptions;

    /// <summary>
    /// Used for building the view component result which will be tested.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IViewComponentResultTestBuilder<TInvocationResult> : IBaseTestBuilderWithViewComponentResult<TInvocationResult>
    {
        /// <summary>
        /// Used for testing the view component's additional data - attributes, HTTP response, view bag and more.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentShouldHaveTestBuilder{TInvocationResult}"/> type.</returns>
        IViewComponentShouldHaveTestBuilder<TInvocationResult> ShouldHave();

        /// <summary>
        /// Used for testing whether the view component throws exception.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldThrowTestBuilder"/>.</returns>
        IShouldThrowTestBuilder ShouldThrow();

        /// <summary>
        /// Used for testing returned view component result.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentShouldReturnTestBuilder{TInvocationResult}"/>.</returns>
        IViewComponentShouldReturnTestBuilder<TInvocationResult> ShouldReturn();
    }
}
