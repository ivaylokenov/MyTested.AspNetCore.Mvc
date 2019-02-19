namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Linq;
    using And;
    using Contracts.And;
    using Contracts.Base;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Validators;

    public abstract class BaseTestBuilderWithModelError : BaseTestBuilderWithActionContext, IBaseTestBuilderWithModelError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithModelError"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelState">Optional <see cref="ModelStateDictionary"/> to use the test builder with. Default is controller's <see cref="ModelStateDictionary"/>.</param>
        protected BaseTestBuilderWithModelError(
            ActionTestContext testContext,
            ModelStateDictionary modelState = null)
            : base(testContext) 
            => this.ModelState = modelState ?? testContext.ModelState;

        /// <summary>
        /// Gets validated <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <value><see cref="ModelStateDictionary"/> containing all validation errors.</value>
        public ModelStateDictionary ModelState { get; private set; }

        /// <inheritdoc />
        public IAndTestBuilder ContainingNoErrors()
        {
            ModelStateValidator.CheckValidModelState(this.TestContext);
            return new AndTestBuilder(this.TestContext);
        }

        protected void ValidateContainingError(string errorKey)
        {
            if (!this.ModelState.Any() || !this.ModelState.ContainsKey(errorKey))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} to have a model error against key '{1}', but in fact none was found.",
                    errorKey);
            }
        }

        protected void ValidateContainingNoError(string errorKey)
        {
            if (this.ModelState.ContainsKey(errorKey))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} to not have a model error against key '{1}', but in fact such was found.",
                    errorKey);
            }
        }

        public void ThrowNewModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.TestContext.ExceptionMessagePrefix,
                    errorKey));
        }
    }
}
