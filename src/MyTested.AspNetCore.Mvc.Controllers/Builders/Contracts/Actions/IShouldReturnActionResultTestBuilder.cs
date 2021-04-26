namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
    using ActionResults.ActionResult;
    using And;

    /// <summary>
    /// Used for testing returned <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldReturnActionResultTestBuilder<TActionResult>
        : IShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ActionResult();

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> or
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
        /// </summary>
        /// <param name="actionResultTestBuilder">Test builder which asserts the actual action result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ActionResult(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder);

        /// <summary>
        /// Tests whether the action result is
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ActionResult<TResult>();

        /// <summary>
        /// Tests whether the action result is
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <param name="actionResultTestBuilder">Test builder which asserts the actual action result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ActionResult<TResult>(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder);

        /// <summary>
        /// Tests whether the action result is
        /// <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <param name="actionResultTestBuilder">Test builder which asserts the actual result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ActionResult<TResult>(Action<IActionResultOfTTestBuilder<TResult>> actionResultTestBuilder);
    }
}
