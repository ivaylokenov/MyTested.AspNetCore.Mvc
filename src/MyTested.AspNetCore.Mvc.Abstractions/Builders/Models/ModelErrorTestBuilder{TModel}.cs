namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing the <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public class ModelErrorTestBuilder<TModel> : ModelErrorTestBuilder, IAndModelErrorTestBuilder<TModel>
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

        /// <summary>
        /// Gets model from invoked method in ASP.NET Core MVC.
        /// </summary>
        /// <value>Model from invoked method.</value>
        protected TModel Model => this.TestContext.ModelAs<TModel>();

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> AndAlso() => this;
        
        public void ThrowNewModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.TestContext.ExceptionMessagePrefix,
                    errorKey));
        }
    }
}
