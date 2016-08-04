namespace MyTested.AspNetCore.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public class ShouldPassForTestBuilderWithModel<TModel> : ShouldPassForTestBuilderWithInvokedAction,
        IShouldPassForTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilderWithModel{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilderWithModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithModel<TModel> TheModel(Action<TModel> assertions)
        {
            assertions(this.GetModel());
            return this;
        }

        /// <inheritdoc />
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
