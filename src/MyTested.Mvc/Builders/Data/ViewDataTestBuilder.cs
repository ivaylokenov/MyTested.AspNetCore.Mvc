namespace MyTested.Mvc.Builders.Data
{
    using Base;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class ViewDataTestBuilder : BaseTestBuilderWithInvokedAction, IAndViewDataTestBuilder
    {
        private readonly ViewDataDictionary viewData;

        public ViewDataTestBuilder(ControllerTestContext testContext)
            :base(testContext)
        {
            this.viewData = testContext.ControllerAs<Controller>().ViewData;
        }
    }
}
