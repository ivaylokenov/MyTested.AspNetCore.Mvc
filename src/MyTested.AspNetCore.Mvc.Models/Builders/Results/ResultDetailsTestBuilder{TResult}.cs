namespace MyTested.AspNetCore.Mvc.Builders.Results
{
    using Contracts.Models;
    using Contracts.Results;
    using Internal.TestContexts;
    using Models;

    /// <summary>
    /// Used for testing the result members.
    /// </summary>
    /// <typeparam name="TResult">Result from invoked method in ASP.NET Core MVC.</typeparam>
    public class ResultDetailsTestBuilder<TResult> 
        : ModelDetailsTestBuilder<TResult>,
        IResultDetailsTestBuilder<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDetailsTestBuilder{TResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        public ResultDetailsTestBuilder(ActionTestContext testContext) 
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TResult> EqualTo(TResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}
