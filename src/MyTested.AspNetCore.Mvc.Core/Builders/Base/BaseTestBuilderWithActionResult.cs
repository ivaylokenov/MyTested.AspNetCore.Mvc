namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using And;
    using Contracts.And;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using ShouldPassFor;
    using Utilities;

    /// <summary>
    /// Base class for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult>
        : BaseTestBuilderWithInvokedAction, IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithActionResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        public TActionResult ActionResult => this.TestContext.MethodResultAs<TActionResult>();
        
        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithActionResult<TActionResult> ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<TActionResult>(this.TestContext);

        /// <summary>
        /// Initializes new instance of builder providing AndAlso method.
        /// </summary>
        /// <returns>Test builder of type <see cref="IAndTestBuilder{TActionResult}"/>.</returns>
        public IAndTestBuilder<TActionResult> NewAndTestBuilder()
        {
            return new AndTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Creates new <see cref="AndProvideTestBuilder{TActionResult}"/>.
        /// </summary>
        /// <returns>Base test builder of type <see cref="IBaseTestBuilderWithActionResult{TActionResult}"/>.</returns>
        public new IBaseTestBuilderWithActionResult<TActionResult> NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Returns the actual action result casted as dynamic type.
        /// </summary>
        /// <returns>Object of dynamic type.</returns>
        protected dynamic GetActionResultAsDynamic()
        {
            return this.ActionResult.GetType().CastTo<dynamic>(this.ActionResult);
        }
    }
}
