namespace MyTested.Mvc.Builders.Models
{
    using System;
    using System.Linq.Expressions;
    using Contracts.Models;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using ShouldPassFor;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing the <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelErrorTestBuilder<TModel> : ModelErrorTestBuilder, IAndModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the class with. Default is Default is <see cref="ControllerBase"/>'s <see cref="ModelStateDictionary"/>.</param>
        public ModelErrorTestBuilder(
            ControllerTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext, modelState)
        {
        }

        /// <summary>
        /// Gets model from invoked action in ASP.NET Core MVC controller.
        /// </summary>
        /// <value>Model from invoked action.</value>
        protected TModel Model => this.TestContext.ModelAs<TModel>();

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey)
        {
            if (!this.ModelState.ContainsKey(errorKey) || this.ModelState.Count == 0)
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    errorKey);
            }

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.TestContext,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError)
        {
            var memberName = ExpressionHelper.GetExpressionText(memberWithError);
            this.ContainingError(memberName);

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.TestContext,
                this,
                memberName,
                this.ModelState[memberName].Errors);
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> ContainingNoErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithNoError)
        {
            var memberName = ExpressionHelper.GetExpressionText(memberWithNoError);
            if (this.ModelState.ContainsKey(memberName))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have no model errors against key {2}, but found some.",
                    memberName);
            }

            return this;
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> AndAlso() => this;

        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithModel<TModel> ShouldPassFor()
            => new ShouldPassForTestBuilderWithModel<TModel>(this.TestContext);
        
        private void ThrowNewModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    errorKey));
        }
    }
}
