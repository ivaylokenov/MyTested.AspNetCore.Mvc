namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Results
{
    using Models;

    /// <summary>
    /// Used for testing the result members.
    /// </summary>
    public interface IResultDetailsTestBuilder
    {
        /// <summary>
        /// Tests whether the result is deeply equal to the provided one.
        /// </summary>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <param name="result">Expected result object.</param>
        /// <returns>Test builder of <see cref="IAndModelDetailsTestBuilder{TModel}"/> type.</returns>
        IAndModelDetailsTestBuilder<TResult> EqualTo<TResult>(TResult result);
    }
}
