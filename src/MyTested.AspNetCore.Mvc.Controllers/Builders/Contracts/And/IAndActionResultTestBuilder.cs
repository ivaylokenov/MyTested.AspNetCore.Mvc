namespace MyTested.AspNetCore.Mvc.Builders.Contracts.And
{
    using Actions;
    using Base;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after the action tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IAndActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the action tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/>.</returns>
        IActionResultTestBuilder<TActionResult> AndAlso();
    }
}
