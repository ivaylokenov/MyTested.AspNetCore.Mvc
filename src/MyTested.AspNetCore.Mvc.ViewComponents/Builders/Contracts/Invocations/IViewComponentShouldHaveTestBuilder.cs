namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using Base;

    public interface IViewComponentShouldHaveTestBuilder<TInvocationResult>
        : IBaseTestBuilderWithComponentShouldHaveTestBuilder<IAndViewComponentResultTestBuilder<TInvocationResult>>
    {
    }
}
