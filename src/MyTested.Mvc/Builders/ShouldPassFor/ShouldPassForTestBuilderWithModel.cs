namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Utilities.Validators;

    public class ShouldPassForTestBuilderWithModel<TModel> : ShouldPassForTestBuilderWithInvokedAction,
        IShouldPassForTestBuilderWithModel<TModel>
    {
        public ShouldPassForTestBuilderWithModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IShouldPassForTestBuilderWithModel<TModel> TheModel(Action<TModel> assertions)
        {
            assertions(this.GetModel());
            return this;
        }

        public IShouldPassForTestBuilderWithModel<TModel> TheModel(Func<TModel, bool> predicate)
        {
            this.ValidateFor(predicate, this.GetModel());
            return this;
        }

        private TModel GetModel()
        {
            var model = this.TestContext.ModelAs<TModel>();

            CommonValidator.CheckForEqualityWithDefaultValue(model, "AndProvideTheModel can be used when there is response model from the action.");

            return model;
        }
    }
}
