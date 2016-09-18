namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Base;
    using Contracts.Models;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Used for testing the <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    public class ModelErrorTestBuilder : BaseTestBuilderWithModelError, IAndModelErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the test builder with. Default is controller's <see cref="ModelStateDictionary"/>.</param>
        public ModelErrorTestBuilder(
            ActionTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext, modelState)
        {
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder ContainingError(string errorKey)
        {
            this.ValidateContainingError(errorKey);

            return new ModelErrorDetailsTestBuilder(
                this.TestContext,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder ContainingNoError(string errorKey)
        {
            this.ValidateContainingNoError(errorKey);
            return this;
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder AndAlso() => this;
    }
}
