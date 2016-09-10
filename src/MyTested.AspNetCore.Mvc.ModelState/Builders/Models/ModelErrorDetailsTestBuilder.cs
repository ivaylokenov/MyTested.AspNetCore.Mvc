namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing specific <see cref="ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelErrorDetailsTestBuilder<TModel> : BaseTestBuilderWithComponent, IModelErrorDetailsTestBuilder<TModel>
    {
        private readonly IAndModelErrorTestBuilder<TModel> modelErrorTestBuilder;
        private readonly string currentErrorKey;
        private readonly IEnumerable<string> aggregatedErrors;

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
            : base(testContext)
        {
            this.modelErrorTestBuilder = modelErrorTestBuilder;
            this.currentErrorKey = errorKey;
            this.aggregatedErrors = aggregatedErrors.Select(me => me.ErrorMessage);
        }

        public IModelErrorTestBuilder<TModel> ModelErrorTestBuilder => this.modelErrorTestBuilder;

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> ThatEquals(string errorMessage)
        {
            if (this.aggregatedErrors.All(e => e != errorMessage))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key {1} to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> BeginningWith(string beginMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.StartsWith(beginMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> EndingWith(string endMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.EndsWith(endMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> Containing(string containsMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.Contains(containsMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <inheritdoc />
        public IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey)
        {
            return this.modelErrorTestBuilder.ContainingError(errorKey);
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TModel> AndAlso() => this.modelErrorTestBuilder;

        private void ThrowNewModelErrorAssertionException(string messageFormat, string operation)
        {
            throw new ModelErrorAssertionException(string.Format(
                messageFormat,
                this.TestContext.ExceptionMessagePrefix,
                this.currentErrorKey,
                operation,
                string.Join(", ", this.aggregatedErrors)));
        }
    }
}
