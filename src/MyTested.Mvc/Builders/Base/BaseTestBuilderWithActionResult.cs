namespace MyTested.Mvc.Builders.Base
{
    using System;
    using And;
    using Contracts.And;
    using Contracts.Base;
    using Internal;
    using Utilities;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult>
        : BaseTestBuilderWithInvokedAction, IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithActionResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        internal TActionResult ActionResult => this.TestContext.ActionResultAs<TActionResult>();

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public TActionResult AndProvideTheActionResult()
        {
            if (this.ActionResult.GetType() == typeof(VoidActionResult))
            {
                throw new InvalidOperationException("Void methods cannot provide action result because they do not have return value.");
            }

            return this.ActionResult;
        }

        /// <summary>
        /// Initializes new instance of builder providing AndAlso method.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        protected IAndTestBuilder<TActionResult> NewAndTestBuilder()
        {
            return new AndTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        protected new IBaseTestBuilderWithActionResult<TActionResult> NewAndProvideTestBuilder()
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
