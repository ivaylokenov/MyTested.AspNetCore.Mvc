namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using Base;
    using CaughtExceptions;

    /// <summary>
    /// Used for testing the action and its result.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Used for testing returned action result.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldReturnTestBuilder{TActionResult}"/>.</returns>
        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}
