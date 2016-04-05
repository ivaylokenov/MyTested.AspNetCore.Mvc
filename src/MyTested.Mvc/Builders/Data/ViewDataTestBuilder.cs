namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    public class ViewDataTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndViewDataTestBuilder>, IAndViewDataTestBuilder
    {
        internal const string ViewDataName = "view data";

        public ViewDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, ViewDataName)
        {
        }

        protected override IAndViewDataTestBuilder DataProviderTestBuilder => this;

        public IViewDataTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.ViewData;
    }
}
