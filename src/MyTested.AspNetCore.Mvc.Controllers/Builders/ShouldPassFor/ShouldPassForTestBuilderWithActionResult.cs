namespace MyTested.AspNetCore.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal;
    using Internal.TestContexts;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ShouldPassForTestBuilderWithActionResult<TActionResult> : ShouldPassForTestBuilderWithInvokedAction,
        IShouldPassForTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilderWithActionResult{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilderWithActionResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Action<TActionResult> assertions)
        {
            assertions(this.GetActionResult());
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Func<TActionResult, bool> predicate)
        {
            this.ValidateFor(predicate, this.GetActionResult());
            return this;
        }

        private TActionResult GetActionResult()
        {
            var actionResult = this.TestContext.MethodResultAs<TActionResult>();

            if (actionResult?.GetType() == typeof(VoidActionResult))
            {
                throw new InvalidOperationException("Void methods cannot provide action result because they do not have return value.");
            }

            return actionResult;
        }
    }
}
