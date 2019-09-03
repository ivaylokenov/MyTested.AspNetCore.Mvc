namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using Base;
    using Internal.Results;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public interface IVoidActionResultTestBuilder : IBaseActionResultTestBuilder<MethodResult>
    {
        /// <summary>
        /// Tests whether the action result is void.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithInvokedAction"/> type.</returns>
        IBaseTestBuilderWithInvokedAction ShouldReturnEmpty();
    }
}
