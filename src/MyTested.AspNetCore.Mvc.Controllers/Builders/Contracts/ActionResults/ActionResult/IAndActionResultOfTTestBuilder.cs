namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.ActionResult
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/> tests.
    /// </summary>
    /// <typeparam name="TResult">Type of the expected result.</typeparam>
    public interface IAndActionResultOfTTestBuilder<TResult>
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IActionResultOfTTestBuilder{TResult}"/>.</returns>
        IActionResultOfTTestBuilder<TResult> AndAlso();
    }
}
