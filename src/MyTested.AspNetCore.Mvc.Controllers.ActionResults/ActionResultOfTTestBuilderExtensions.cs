namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.ActionResult;
    using Builders.Contracts.ActionResults.ActionResult;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IActionResultOfTTestBuilder{TResult}"/>.
    /// </summary>
    public static class ActionResultOfTTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>
        /// has a result deeply equal to the provided one.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <param name="actionResultTestBuilder">
        /// Instance of <see cref="IActionResultOfTTestBuilder{TResult}"/> type.
        /// </param>
        /// <param name="result">Expected result object.</param>
        /// <returns>The same <see cref="IAndActionResultOfTTestBuilder{TResult}"/>.</returns>
        public static IAndActionResultOfTTestBuilder<TResult> EqualTo<TResult>(
            this IActionResultOfTTestBuilder<TResult> actionResultTestBuilder,
            TResult result)
        {
            var actualBuilder = (ActionResultOfTTestBuilder<TResult>)actionResultTestBuilder;

            InvocationResultValidator.ValidateInvocationResult(
                actualBuilder.TestContext,
                result,
                canBeAssignable: true);

            return actualBuilder;
        }
    }
}
