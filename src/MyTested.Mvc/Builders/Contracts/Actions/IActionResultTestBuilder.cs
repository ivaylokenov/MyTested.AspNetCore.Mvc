namespace MyTested.Mvc.Builders.Contracts.Actions
{
    using Base;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Used for testing action attributes and model state.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        IShouldHaveTestBuilder<TActionResult> ShouldHave();

        /// <summary>
        /// Used for testing whether action throws exception.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        IShouldThrowTestBuilder ShouldThrow();

        /// <summary>
        /// Used for testing returned action result.
        /// </summary>
        /// <returns>Should return test builder.</returns>
        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}
