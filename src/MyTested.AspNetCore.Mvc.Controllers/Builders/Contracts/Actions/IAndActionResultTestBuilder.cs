namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    /// <summary>
    /// Contains AndAlso() method allowing additional assertions after the action result tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IAndActionResultTestBuilder<TActionResult> : IShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the action tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/>.</returns>
        IActionResultTestBuilder<TActionResult> AndAlso();
    }
}
