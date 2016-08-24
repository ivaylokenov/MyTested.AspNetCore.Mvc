namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using Base;
    using Internal;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public interface IVoidActionResultTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the action result is void.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithInvokedAction"/> type.</returns>
        IBaseTestBuilderWithInvokedAction ShouldReturnEmpty();

        /// <summary>
        /// Used for testing the action's additional data - action attributes, HTTP response, view bag and more.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldHaveTestBuilder{VoidActionResult}"/> type.</returns>
        IShouldHaveTestBuilder<VoidMethodResult> ShouldHave();

        /// <summary>
        /// Used for testing whether the action throws exception.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldThrowTestBuilder"/> type.</returns>
        IShouldThrowTestBuilder ShouldThrow();
    }
}
