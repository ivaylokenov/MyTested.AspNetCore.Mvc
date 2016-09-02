namespace MyTested.AspNetCore.Mvc.Builders.ViewComponentResults
{
    using System;
    using Base;
    using Contracts.ViewComponentResults;
    using Internal.TestContexts;

    public class ViewTestBuilder : BaseTestBuilderWithResponseModel, IViewTestBuilder
    {
        public ViewTestBuilder(ActionTestContext testContext)
            : base(testContext)
        {
        }

        protected override TModel GetActualModel<TModel>()
        {
            throw new NotImplementedException();
        }

        protected override Type GetReturnType()
        {
            throw new NotImplementedException();
        }
    }
}
