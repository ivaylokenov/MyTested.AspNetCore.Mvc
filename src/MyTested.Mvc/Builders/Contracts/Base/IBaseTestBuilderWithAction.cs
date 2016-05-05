namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with action call.
    /// </summary>
    public interface IBaseTestBuilderWithAction : IBaseTestBuilderWithController
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithAction"/> type.</returns>
        new IShouldPassForTestBuilderWithAction ShouldPassFor();
    }
}
