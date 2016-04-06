namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base class for all test builders with controller.
    /// </summary>
    public interface IBaseTestBuilderWithController : IBaseTestBuilder
    {
        new IShouldPassForTestBuilderWithController<object> ShouldPassFor();
    }
}
