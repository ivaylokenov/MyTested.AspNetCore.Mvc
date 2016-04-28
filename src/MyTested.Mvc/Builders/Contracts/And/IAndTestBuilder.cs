namespace MyTested.Mvc.Builders.Contracts.And
{
    using Actions;
    using Base;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after action tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IAndTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the action tests.
        /// </summary>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> AndAlso();
    }
}
