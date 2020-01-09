namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using And;
    using Contracts.Actions;
    using Contracts.And;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing returned <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ShouldReturnActionResultTestBuilder<TActionResult>
        : ShouldReturnTestBuilder<TActionResult>, 
        IShouldReturnActionResultTestBuilder<TActionResult>
    {
        private static readonly Type ActionResultType = typeof(IActionResult);
        private static readonly Type GenericActionResultType = typeof(ActionResult<>);

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldReturnActionResultTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldReturnActionResultTestBuilder(ControllerTestContext testContext) 
            : base(testContext)
        {
        }

        /// <inheritdoc />
        IAndTestBuilder IShouldReturnActionResultTestBuilder<TActionResult>.ActionResult()
        {
            this.ValidateActionResults();

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        IAndTestBuilder IShouldReturnActionResultTestBuilder<TActionResult>.ActionResult(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder)
        {
            this.ValidateActionResults();

            actionResultTestBuilder?.Invoke(this);

            return new AndTestBuilder(this.TestContext);
        }

        IAndTestBuilder IShouldReturnActionResultTestBuilder<TActionResult>.ActionResult<TResult>()
        {
            InvocationResultValidator.ValidateInvocationResultType<ActionResult<TResult>>(
                this.TestContext,
                typeOfActualReturnValue: this.TestContext.Method.ReturnType);

            return new AndTestBuilder(this.TestContext);
        }

        private void ValidateActionResults() 
            => InvocationResultValidator.ValidateInvocationResultTypes(
                this.TestContext,
                canBeAssignable: true,
                typesOfExpectedReturnValue: new[] { ActionResultType, GenericActionResultType });
    }
}
