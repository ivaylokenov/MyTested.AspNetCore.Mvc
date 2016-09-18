namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using Base;
    using Contracts.Models;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Used for testing specific <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelErrorDetailsTestBuilder<TModel> : BaseTestBuilderWithModelErrorDetails, IModelErrorDetailsTestBuilder<TModel>
    {
        private readonly IAndModelErrorTestBuilder<TModel> modelErrorTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorDetailsTestBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelErrorTestBuilder">Test builder of <see cref="IAndModelErrorTestBuilder{TModel}"/> type.</param>
        /// <param name="errorKey">Key in <see cref="ModelStateDictionary"/> corresponding to this particular error.</param>
        /// <param name="aggregatedErrors">All errors found in <see cref="ModelStateDictionary"/> for given error key.</param>
        public ModelErrorDetailsTestBuilder(
            ActionTestContext testContext,
            IAndModelErrorTestBuilder<TModel> modelErrorTestBuilder,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(testContext, errorKey, aggregatedErrors)
        {
            this.modelErrorTestBuilder = modelErrorTestBuilder;
        }

        public IModelErrorTestBuilder<TModel> ModelErrorTestBuilder => this.modelErrorTestBuilder;

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> ThatEquals(string errorMessage)
        {
            this.ValidateEquals(errorMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> BeginningWith(string beginMessage)
        {
            this.ValidateBeginning(beginMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> EndingWith(string endMessage)
        {
            this.ValidateEnding(endMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> Containing(string containsMessage)
        {
            this.ValidateContaining(containsMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey)
        {
            return this.modelErrorTestBuilder.ContainingError(errorKey);
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> AndAlso() => this.modelErrorTestBuilder;
    }
}
