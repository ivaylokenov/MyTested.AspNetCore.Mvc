namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for test builders with invoked action.
    /// </summary>
    public interface IBaseTestBuilderWithInvokedAction : IBaseTestBuilderWithAction
    {
        new IShouldPassForTestBuilderWithInvokedAction ShouldPassFor();
    }
}
