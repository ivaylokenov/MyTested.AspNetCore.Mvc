namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
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

        //// IActionResult, ActionResult or ActionResult<T> with options to specify the result - Ok() for example
        //IAndTestBuilder ActionResult(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder);

        //// ActionResult<TResult>, consider OkResult for example to be valid too? 
        //IAndTestBuilder ActionResult<TResult>();

        //// ActionResult<TResult>, consider OkResult for example to be valid too? with additional options to specify the result - Ok() for example
        //IAndTestBuilder ActionResult<TResult>(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder);

        //// ActionResult<TResult>, consider OkResult for example to be valid too?!, with additional options to validate the TResult, like EqualTo or Passing (model/result details)
        //IAndTestBuilder ActionResult<TResult>(Action<IShouldHaveTestBuilder<TActionResult>> actionResultTestBuilder);
    }
}
