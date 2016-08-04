namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Base;
    using Contracts.Models;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Used for testing the <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    public class ModelErrorTestBuilder : BaseTestBuilderWithInvokedAction, IModelErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the test builder with. Default is controller's <see cref="ModelStateDictionary"/>.</param>
        public ModelErrorTestBuilder(
            ControllerTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext)
        {
            this.ModelState = modelState ?? testContext.ModelState;
        }

        /// <summary>
        /// Gets validated <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <value><see cref="ModelStateDictionary"/> containing all validation errors.</value>
        public ModelStateDictionary ModelState { get; private set; }
    }
}
