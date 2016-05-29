namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
    /// </summary>
    public class ViewBagTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndViewBagTestBuilder>, IAndViewBagTestBuilder
    {
        internal const string ViewBagName = "view bag";

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBagTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewBagTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewBagName)
        {
        }

        /// <summary>
        /// Gets the data provider test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndViewBagTestBuilder"/>.</value>
        protected override IAndViewBagTestBuilder DataProviderTestBuilder => this;

        /// <inheritdoc />
        public IViewBagTestBuilder AndAlso() => this;

        /// <summary>
        /// When overridden in derived class provides a way to built the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Data provider as <see cref="IDictionary{TKey,TValue}"/></returns>
        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.ViewData;
    }
}
