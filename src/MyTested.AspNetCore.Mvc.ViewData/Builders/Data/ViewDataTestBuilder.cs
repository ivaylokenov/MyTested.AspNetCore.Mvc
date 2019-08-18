namespace MyTested.AspNetCore.Mvc.Builders.Data
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

        private readonly ComponentTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewDataTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewDataTestBuilder(ComponentTestContext testContext)
            : base(testContext, ViewDataName) 
            => this.testContext = testContext;

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
        protected override IDictionary<string, object> GetDataProvider() => this.testContext.GetViewData();
    }
}
