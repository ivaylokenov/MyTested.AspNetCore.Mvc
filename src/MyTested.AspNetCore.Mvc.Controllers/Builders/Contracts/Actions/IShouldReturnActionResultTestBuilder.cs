namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
    using And;

    public interface IShouldReturnActionResultTestBuilder<TActionResult>
        : IShouldReturnTestBuilder<TActionResult>
    {
        // IActionResult, ActionResult or ActionResult<T>
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
