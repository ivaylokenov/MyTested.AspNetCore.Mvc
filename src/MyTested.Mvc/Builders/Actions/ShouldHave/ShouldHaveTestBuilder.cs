namespace MyTested.Mvc.Builders.Actions.ShouldHave
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
        /// Initializes a new instance of the <see cref="ShouldHaveTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
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

        private void ValidateDataProviderNumberOfEntries(string name, int? expectedCount, int actualCount)
        {
            if (actualCount == 0
                || (expectedCount != null && actualCount != expectedCount))
            {
                this.ThrowNewDataProviderAssertionException(
                    name,
                    expectedCount == null ? " entries" : $" with {expectedCount} {(expectedCount != 1 ? "entries" : "entry")}",
                    expectedCount == null ? "none were found" : $"in fact contained {actualCount}");
            }
        }

        private void ThrowNewDataProviderAssertionExceptionWithNoEntries(string name)
        {
            this.ThrowNewDataProviderAssertionException(
                name,
                " with no entries",
                "in fact it had some");
        }

        private void ThrowNewDataProviderAssertionException(string name, string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                "When calling {0} action in {1} expected to have {2}{3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                name,
                expectedValue,
                actualValue));
        }
    }
}
