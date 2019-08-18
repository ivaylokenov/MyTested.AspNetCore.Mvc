namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using System;
    using And;

    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    public interface IBaseTestBuilderWithActionResult : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder PassingAs<TActionResult>(Action<TActionResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder PassingAs<TActionResult>(Func<TActionResult, bool> predicate);
    }
}
