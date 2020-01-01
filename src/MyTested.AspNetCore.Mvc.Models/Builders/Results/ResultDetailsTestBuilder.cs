namespace MyTested.AspNetCore.Mvc.Builders.Results
{
    using Base;
    using Contracts.Models;
    using Contracts.Results;
    using Internal.TestContexts;
    using Models;
    using Utilities.Validators;

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
            InvocationResultValidator.ValidateInvocationResult(
                this.TestContext,
                result);

            return new ModelDetailsTestBuilder<TResult>(this.TestContext);
        }
    }
}
