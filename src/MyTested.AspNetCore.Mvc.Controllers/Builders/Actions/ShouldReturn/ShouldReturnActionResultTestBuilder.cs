namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.ActionResult;
    using And;
    using Contracts.ActionResults.ActionResult;
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
        public new IAndTestBuilder ActionResult()
        {
            this.ValidateActionResults();

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IAndTestBuilder ActionResult(Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder)
        {
            this.ValidateActionResults();

            actionResultTestBuilder?.Invoke(this);

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IAndTestBuilder ActionResult<TResult>()
        {
            this.ValidateActionResult<TResult>();

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IAndTestBuilder ActionResult<TResult>(
            Action<IShouldReturnTestBuilder<TActionResult>> actionResultTestBuilder)
        {
            this.ValidateActionResult<TResult>();

            actionResultTestBuilder?.Invoke(this);

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IAndTestBuilder ActionResult<TResult>(Action<IActionResultOfTTestBuilder<TResult>> actionResultTestBuilder)
        {
            this.ValidateActionResult<TResult>();

            actionResultTestBuilder?.Invoke(new ActionResultOfTTestBuilder<TResult>(this.TestContext));

            return new AndTestBuilder(this.TestContext);
        }

        private void ValidateActionResults() 
            => InvocationResultValidator.ValidateInvocationResultTypes(
                this.TestContext,
                canBeAssignable: true,
                typesOfExpectedReturnValue: new[] { ActionResultType, GenericActionResultType });

        private void ValidateActionResult<TResult>()
            => InvocationResultValidator.ValidateInvocationResultType<ActionResult<TResult>>(
                this.TestContext,
                typeOfActualReturnValue: this.TestContext.Method.ReturnType);
    }
}
