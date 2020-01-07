namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
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
        : ShouldReturnTestBuilder<TActionResult>, IShouldReturnActionResultTestBuilder<TActionResult>
    {
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
            InvocationResultValidator.ValidateInvocationResultTypes(
                this.TestContext,
                canBeAssignable: true,
                typesOfExpectedReturnValue: new [] { typeof(IActionResult), typeof(ActionResult<>) });

            return new AndTestBuilder(this.TestContext);
        }
    }
}
