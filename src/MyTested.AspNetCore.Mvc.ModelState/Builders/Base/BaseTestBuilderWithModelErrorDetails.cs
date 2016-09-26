namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public abstract class BaseTestBuilderWithModelErrorDetails : BaseTestBuilderWithComponent
    {
        private readonly string currentErrorKey;
        private readonly IEnumerable<string> aggregatedErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithModelErrorDetails"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="errorKey">Key in <see cref="ModelStateDictionary"/> corresponding to this particular error.</param>
        /// <param name="aggregatedErrors">All errors found in <see cref="ModelStateDictionary"/> for given error key.</param>
        protected BaseTestBuilderWithModelErrorDetails(
            ActionTestContext testContext,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(testContext)
        {
            this.currentErrorKey = errorKey;
            this.aggregatedErrors = aggregatedErrors.Select(me => me.ErrorMessage);
        }

        protected void ValidateEquals(string errorMessage)
        {
            if (this.aggregatedErrors.All(e => e != errorMessage))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key {1} to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }
        }

        protected void ValidateBeginning(string beginMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.StartsWith(beginMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }
        }

        protected void ValidateEnding(string endMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.EndsWith(endMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }
        }

        protected void ValidateContaining(string containsMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.Contains(containsMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "{0} error message for key '{1}' to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }
        }

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
