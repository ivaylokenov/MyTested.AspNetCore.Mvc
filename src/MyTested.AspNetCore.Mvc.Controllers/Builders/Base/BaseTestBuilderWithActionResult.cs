﻿namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using Contracts.And;
    using Contracts.Base;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult> 
        : BaseTestBuilderWithInvokedAction, 
        IBaseTestBuilderWithActionResult<TActionResult>
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

        /// <summary>
        /// Gets the action result which will be tested as object.
        /// </summary>
        /// <value>Action result to be tested as object.</value>
        protected object ObjectActionResult => this.TestContext.MethodResult;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<TActionResult> assertions)
            => this.Passing<TActionResult>(assertions);

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<TActionResult, bool> predicate)
            => this.Passing<TActionResult>(predicate);

        /// <inheritdoc />
        public IAndTestBuilder PassingAs<TActionResultAs>(Action<TActionResultAs> assertions)
            => this.Passing(assertions);

        /// <inheritdoc />
        public IAndTestBuilder PassingAs<TActionResultAs>(Func<TActionResultAs, bool> predicate)
            => this.Passing(predicate);
    }
}
