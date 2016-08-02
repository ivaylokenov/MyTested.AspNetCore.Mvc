namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base class for all test builders with controller.
    /// </summary>
    public interface IBaseTestBuilderWithController : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithController{TController}"/> type.</returns>
        new IShouldPassForTestBuilderWithController<object> ShouldPassFor();
    }
}
