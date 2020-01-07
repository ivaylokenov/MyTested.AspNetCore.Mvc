namespace MyTested.AspNetCore.Mvc.Builders.Pipeline
{
    using Builders.Actions;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Builders.Contracts.CaughtExceptions;
    using Builders.Contracts.Pipeline;
    using Builders.Controllers;
    using Internal.Results;
    using Internal.TestContexts;
    using Utilities;

    /// <summary>
    /// Used for building the controller which will be tested after a route assertion.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public class WhichControllerInstanceBuilder<TController>
        : BaseControllerBuilder<TController, IAndWhichControllerInstanceBuilder<TController>>,
        IAndWhichControllerInstanceBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhichControllerInstanceBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public WhichControllerInstanceBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<MethodResult> ShouldHave()
            => this
                .InvokeAndGetActionResultTestBuilder()
                .ShouldHave();

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
            => this
                .InvokeAndGetActionResultTestBuilder()
                .ShouldThrow();

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction ShouldReturnEmpty()
            => this
                .InvokeAndGetActionResultTestBuilder()
                .ShouldReturnEmpty();

        /// <inheritdoc />
        public IShouldReturnActionResultTestBuilder<MethodResult> ShouldReturn()
        {
            this.InvokeAction();

            var actionResultTestBuilder = new ActionResultTestBuilder<MethodResult>(this.TestContext);

            return actionResultTestBuilder.ShouldReturn();
        }

        /// <inheritdoc />
        public IWhichControllerInstanceBuilder<TController> AndAlso() => this;

        protected override IAndWhichControllerInstanceBuilder<TController> SetBuilder() => this;

        private VoidActionResultTestBuilder InvokeAndGetActionResultTestBuilder()
        {
            this.InvokeAction();

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        private void InvokeAction()
            => ActionCallExpressionInvoker<TController>
                .Invoke(this.TestContext.MethodCall, this);
    }
}
