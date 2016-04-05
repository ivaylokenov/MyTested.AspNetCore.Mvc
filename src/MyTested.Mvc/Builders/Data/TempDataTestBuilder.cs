namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;

    public class TempDataTestBuilder : BaseDataProviderWithStringKeyTestBuilder<IAndTempDataTestBuilder>, IAndTempDataTestBuilder
    {
        internal const string TempDataName = "temp data";

        public TempDataTestBuilder(ControllerTestContext testContext)
            : base(testContext, TempDataName)
        {
        }

        protected override IAndTempDataTestBuilder DataProviderTestBuilder => this;

        public ITempDataTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider() => this.TestContext.TempData;
    }
}
