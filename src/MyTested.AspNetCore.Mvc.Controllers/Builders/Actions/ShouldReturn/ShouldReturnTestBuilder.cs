namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Challenge;
    using And;
    using Base;
    using Contracts.Actions;
    using Contracts.And;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing returned action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldReturnTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldReturnTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IAndTestBuilder ValidateActionResult<TResult, TTestBuilder>(
            Action<TTestBuilder> testBuilderAction = null,
            TTestBuilder testBuilder = null)
            where TTestBuilder : class
        {
            InvocationResultValidator.ValidateInvocationResultType<TResult>(this.TestContext);

            testBuilderAction?.Invoke(testBuilder);

            return new AndTestBuilder(this.TestContext);
        }
    }
}
