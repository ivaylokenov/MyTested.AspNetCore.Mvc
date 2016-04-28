namespace MyTested.Mvc.Builders.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing specific model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelErrorDetailsTestBuilder<TModel> : BaseTestBuilderWithModel<TModel>, IModelErrorDetailsTestBuilder<TModel>
    {
        private readonly IAndModelErrorTestBuilder<TModel> modelErrorTestBuilder;
        private readonly string currentErrorKey;
        private readonly IEnumerable<string> aggregatedErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorDetailsTestBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="model">Model returned from action result.</param>
        /// <param name="modelErrorTestBuilder">Original model error test builder.</param>
        /// <param name="errorKey">Key in ModelStateDictionary corresponding to this particular error.</param>
        /// <param name="aggregatedErrors">All errors found in ModelStateDictionary for given error key.</param>
        public ModelErrorDetailsTestBuilder(
            ControllerTestContext testContext,
            IAndModelErrorTestBuilder<TModel> modelErrorTestBuilder,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(testContext)
        {
            this.modelErrorTestBuilder = modelErrorTestBuilder;
            this.currentErrorKey = errorKey;
            this.aggregatedErrors = aggregatedErrors.Select(me => me.ErrorMessage);
        }

        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>The original model error test builder.</returns>
        public IAndModelErrorTestBuilder<TModel> ThatEquals(string errorMessage)
        {
            if (this.aggregatedErrors.All(e => e != errorMessage))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key {2} to be '{3}', but instead found '{4}'.",
                    errorMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        public IAndModelErrorTestBuilder<TModel> BeginningWith(string beginMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.StartsWith(beginMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to begin with '{3}', but instead found '{4}'.",
                    beginMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        public IAndModelErrorTestBuilder<TModel> EndingWith(string endMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.EndsWith(endMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to end with '{3}', but instead found '{4}'.",
                    endMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        public IAndModelErrorTestBuilder<TModel> Containing(string containsMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.Contains(containsMessage)))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to contain '{3}', but instead found '{4}'.",
                    containsMessage);
            }

            return this.modelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey)
        {
            return this.modelErrorTestBuilder.ContainingError(errorKey);
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError)
        {
            return this.modelErrorTestBuilder.ContainingErrorFor(memberWithError);
        }

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorTestBuilder<TModel> ContainingNoErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithNoError)
        {
            return this.modelErrorTestBuilder.ContainingNoErrorFor(memberWithNoError);
        }

        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorTestBuilder<TModel> AndAlso()
        {
            return this.modelErrorTestBuilder;
        }

        private void ThrowNewModelErrorAssertionException(string messageFormat, string operation)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    this.currentErrorKey,
                    operation,
                    string.Join(", ", this.aggregatedErrors)));
        }
    }
}
