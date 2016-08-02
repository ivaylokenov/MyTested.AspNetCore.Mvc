namespace MyTested.AspNetCore.Mvc.Builders.ShouldPassFor
{
    using System;
    using System.Collections.Generic;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public class ShouldPassForTestBuilderWithController<TController> : ShouldPassForTestBuilder,
        IShouldPassForTestBuilderWithController<TController>
        where TController : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilderWithController{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilderWithController(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets the currently used <see cref="ControllerTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ControllerTestContext"/>.</value>
        protected ControllerTestContext TestContext { get; private set; }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithController<TController> TheController(Action<TController> assertions)
        {
            assertions(this.TestContext.ComponentAs<TController>());
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithController<TController> TheController(Func<TController, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ComponentAs<TController>());
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Action<IEnumerable<object>> assertions)
        {
            assertions(this.TestContext.ComponentAttributes);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ComponentAttributes, "controller attributes");
            return this;
        }
    }
}
