namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldPassForTestBuilderWithActionResult<TActionResult> : IShouldPassForTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the action result passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action delegate containing assertions on the action result.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithActionResult{TActionResult}"/>.</returns>
        IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Action<TActionResult> assertions);

        /// <summary>
        /// Tests whether the action result passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the action result.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithActionResult{TActionResult}"/>.</returns>
        IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Func<TActionResult, bool> predicate);
    }
}
