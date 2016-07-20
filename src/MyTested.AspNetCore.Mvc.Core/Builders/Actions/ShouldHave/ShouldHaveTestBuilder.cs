namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Base;
    using Contracts.Actions;
    using Contracts.And;
    using Contracts.Http;
    using Exceptions;
    using Http;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing action attributes and controller properties.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldHaveTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldHaveTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> HttpResponse(Action<IHttpResponseTestBuilder> httpResponseTestBuilder)
        {
            httpResponseTestBuilder(new HttpResponseTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
