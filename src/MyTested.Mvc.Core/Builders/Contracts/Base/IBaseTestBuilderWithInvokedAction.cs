namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for test builders with invoked action.
    /// </summary>
    public interface IBaseTestBuilderWithInvokedAction : IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithInvokedAction"/> type.</returns>
        new IShouldPassForTestBuilderWithInvokedAction ShouldPassFor();
    }
}
