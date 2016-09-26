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
    public class ModelErrorDetailsTestBuilder : BaseTestBuilderWithModelErrorDetails, IModelErrorDetailsTestBuilder
    {
        private readonly IAndModelErrorTestBuilder modelErrorTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorDetailsTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="modelErrorTestBuilder">Test builder of <see cref="IAndModelErrorTestBuilder"/> type.</param>
        /// <param name="errorKey">Key in <see cref="ModelStateDictionary"/> corresponding to this particular error.</param>
        /// <param name="aggregatedErrors">All errors found in <see cref="ModelStateDictionary"/> for given error key.</param>
        public ModelErrorDetailsTestBuilder(
            ActionTestContext testContext,
            IAndModelErrorTestBuilder modelErrorTestBuilder,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(testContext, errorKey, aggregatedErrors)
        {
            this.modelErrorTestBuilder = modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder ThatEquals(string errorMessage)
        {
            this.ValidateEquals(errorMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder BeginningWith(string beginMessage)
        {
            this.ValidateBeginning(beginMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder EndingWith(string endMessage)
        {
            this.ValidateEnding(endMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder Containing(string containsMessage)
        {
            this.ValidateContaining(containsMessage);
            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder ContainingError(string errorKey)
        {
            return this.modelErrorTestBuilder.ContainingError(errorKey);
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder AndAlso() => this.modelErrorTestBuilder;
    }
}
