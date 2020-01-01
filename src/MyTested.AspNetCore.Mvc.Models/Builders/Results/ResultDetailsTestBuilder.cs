namespace MyTested.AspNetCore.Mvc.Builders.Results
{
    using Base;
    using Contracts.Models;
    using Contracts.Results;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing the result members.
    /// </summary>
    public class ResultDetailsTestBuilder 
        : BaseTestBuilderWithActionContext, 
        IResultDetailsTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDetailsTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        public ResultDetailsTestBuilder(ActionTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TResult> EqualTo<TResult>(TResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}
