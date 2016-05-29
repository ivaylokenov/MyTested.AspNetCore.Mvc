namespace MyTested.Mvc.Builders.Contracts.Actions
{
    using Base;

    /// <summary>
    /// Used for testing the action and its result.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
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

        /// <summary>
        /// Used for testing returned action result.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldReturnTestBuilder{TActionResult}"/>.</returns>
        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}
