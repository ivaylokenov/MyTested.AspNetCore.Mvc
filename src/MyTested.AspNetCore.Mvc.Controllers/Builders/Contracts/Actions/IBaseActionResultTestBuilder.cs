namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using Base;
    using CaughtExceptions;

    /// <summary>
    /// Base interface for action result test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IBaseActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Used for testing the action's additional data - action attributes, HTTP response, view bag and more.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</returns>
        IShouldHaveTestBuilder<TActionResult> ShouldHave();

        /// <summary>
        /// Used for testing whether the action throws exception.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldThrowTestBuilder"/>.</returns>
        IShouldThrowTestBuilder ShouldThrow();
    }
}
