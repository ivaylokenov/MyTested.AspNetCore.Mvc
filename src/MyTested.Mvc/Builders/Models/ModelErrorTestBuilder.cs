namespace MyTested.Mvc.Builders.Models
{
    using Base;
    using Contracts.Base;
    using Contracts.Models;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing the model errors.
    /// </summary>
    public class ModelErrorTestBuilder : BaseTestBuilderWithInvokedAction, IModelErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="modelState">Optional model state dictionary to use the class with. Default is controller's model state.</param>
        public ModelErrorTestBuilder(
            ControllerTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext)
        {
            this.ModelState = modelState ?? testContext.ModelState;
        }

        /// <summary>
        /// Gets validated model state of the provided ASP.NET MVC controller instance.
        /// </summary>
        /// <value>Model state dictionary containing all validation errors.</value>
        protected ModelStateDictionary ModelState { get; private set; }

        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithInvokedAction ContainingNoErrors()
        {
            this.CheckValidModelState();
            return this.NewAndProvideTestBuilder();
        }
    }
}
