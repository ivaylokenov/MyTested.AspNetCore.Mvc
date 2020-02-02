namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Results
{
    using Models;

    /// <summary>
    /// Used for testing the result members.
    /// </summary>
    /// <typeparam name="TResult">Result from invoked method in ASP.NET Core MVC.</typeparam>
    public interface IResultDetailsTestBuilder<TResult> : IModelDetailsTestBuilder<TResult>
    {
        /// <summary>
        /// Tests whether the result is deeply equal to the provided one.
        /// </summary>
        /// <param name="result">Expected result object.</param>
        /// <returns>Test builder of <see cref="IAndModelDetailsTestBuilder{TResult}"/> type.</returns>
        IAndModelDetailsTestBuilder<TResult> EqualTo(TResult result);
    }
}
