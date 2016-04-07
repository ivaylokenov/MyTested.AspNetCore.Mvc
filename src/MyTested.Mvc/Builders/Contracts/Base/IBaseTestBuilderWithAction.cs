namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Contracts.ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with action call.
    /// </summary>
    public interface IBaseTestBuilderWithAction : IBaseTestBuilderWithController
    {
        new IShouldPassForTestBuilderWithAction ShouldPassFor();
    }
}
