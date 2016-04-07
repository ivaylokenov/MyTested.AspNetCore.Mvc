namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for test builders with caught exception.
    /// </summary>
    public interface IBaseTestBuilderWithInvokedAction : IBaseTestBuilderWithAction
    {
        new IShouldPassForTestBuilderWithInvokedAction ShouldPassFor();
    }
}
