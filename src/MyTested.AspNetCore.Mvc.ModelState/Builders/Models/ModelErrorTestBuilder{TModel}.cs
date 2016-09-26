namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Contracts.Models;
    using Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Used for testing the <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public class ModelErrorTestBuilder<TModel> : BaseTestBuilderWithModelError, IAndModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the class with. Default is Default is <see cref="ControllerBase"/>'s <see cref="ModelStateDictionary"/>.</param>
        public ModelErrorTestBuilder(
            ActionTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext, modelState)
        {
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey)
        {
            this.ValidateContainingError(errorKey);

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.TestContext,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> ContainingNoError(string errorKey)
        {
            this.ValidateContainingNoError(errorKey);
            return this;
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> AndAlso() => this;
    }
}
