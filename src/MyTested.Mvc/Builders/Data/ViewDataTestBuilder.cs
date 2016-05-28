namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
    /// </summary>
    public class ViewDataTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndViewDataTestBuilder>, IAndViewDataTestBuilder
    {
        internal const string ViewDataName = "view data";

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewDataTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewDataName)
        {
        }

        /// <summary>
        /// Gets the data provider test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndViewDataTestBuilder"/>.</value>
        protected override IAndViewDataTestBuilder DataProviderTestBuilder => this;

        /// <inheritdoc />
        public IViewDataTestBuilder AndAlso() => this;

        /// <summary>
        /// When overridden in derived class provides a way to built the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Data provider as <see cref="IDictionary{TKey,TValue}"/></returns>
        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.ViewData;
    }
}
