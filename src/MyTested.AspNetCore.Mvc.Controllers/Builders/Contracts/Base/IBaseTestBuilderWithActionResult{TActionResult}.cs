namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using System;
    using And;

    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<TActionResult> : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<TActionResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<TActionResult, bool> predicate);
    }
}
