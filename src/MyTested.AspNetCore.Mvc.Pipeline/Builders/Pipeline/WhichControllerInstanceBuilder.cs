namespace MyTested.AspNetCore.Mvc.Builders.Pipeline
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Builders.Contracts.CaughtExceptions;
    using Builders.Contracts.Pipeline;
    using Builders.Controllers;
    using Internal.Results;
    using Internal.TestContexts;

    public class WhichControllerInstanceBuilder<TController>
        : BaseControllerBuilder<TController, IAndWhichControllerInstanceBuilder<TController>>,
        IAndWhichControllerInstanceBuilder<TController>
        where TController : class
    {
        public WhichControllerInstanceBuilder(ControllerTestContext testContext) 
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<MethodResult> ShouldHave()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IShouldReturnTestBuilder<MethodResult> ShouldReturn()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction ShouldReturnEmpty()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IWhichControllerInstanceBuilder<TController> AndAlso() => this;

        protected override IAndWhichControllerInstanceBuilder<TController> SetBuilder() => this;
    }
}
