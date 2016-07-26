namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using And;
    using Base;
    using Contracts.Actions;
    using Contracts.And;
    using Contracts.Http;
    using Http;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing action attributes and controller properties.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
        : BaseTestBuilderWithComponentShouldHaveTestBuilder<IAndActionResultTestBuilder<TActionResult>>, IShouldHaveTestBuilder<TActionResult>
    {
        private ControllerTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldHaveTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldHaveTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }
        
        /// <summary>
        /// Gets the currently used <see cref="ControllerTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ControllerTestContext"/>.</value>
        public new ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value.Component, nameof(this.TestContext.Component));
                this.testContext = value;
            }
        }

        protected override IAndActionResultTestBuilder<TActionResult> SetBuilder()
            => new AndActionResultTestBuilder<TActionResult>(this.TestContext);

        /// <inheritdoc />
        public IAndActionResultTestBuilder<TActionResult> HttpResponse(Action<IHttpResponseTestBuilder> httpResponseTestBuilder)
        {
            httpResponseTestBuilder(new HttpResponseTestBuilder(this.TestContext));
            return this.Builder;
        }
    }
}
