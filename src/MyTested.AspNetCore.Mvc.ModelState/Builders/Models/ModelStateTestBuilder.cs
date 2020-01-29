namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Contracts.Models;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Used for testing <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    public class ModelStateTestBuilder : ModelErrorTestBuilder, IModelStateTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the class with. Default is Default is <see cref="ControllerBase"/>'s <see cref="ModelStateDictionary"/>.</param>
        public ModelStateTestBuilder(
            ActionTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext, modelState)
        {
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> For<TModel>() 
            => new ModelErrorTestBuilder<TModel>(this.TestContext, this.ModelState);
    }
}
