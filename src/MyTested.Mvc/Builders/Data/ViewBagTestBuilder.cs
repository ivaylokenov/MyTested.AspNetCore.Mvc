namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    public class ViewBagTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndViewBagTestBuilder>, IAndViewBagTestBuilder
    {
        internal const string ViewBagName = "view bag";

        public ViewBagTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewBagName)
        {
        }

        protected override IAndViewBagTestBuilder DataProviderTestBuilder => this;

        public IViewBagTestBuilder AndAlso() => this;
        
        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.ViewData;
    }
}
