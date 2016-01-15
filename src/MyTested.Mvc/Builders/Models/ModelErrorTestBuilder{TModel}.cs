namespace MyTested.Mvc.Builders.Models
{
    using System;
    using System.Linq.Expressions;
    using Contracts.Models;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing the model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public class ModelErrorTestBuilder<TModel> : ModelErrorTestBuilder, IAndModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="model">Model returned from action result.</param>
        /// <param name="modelState">Optional model state dictionary to use the class with. Default is controller's model state.</param>
        public ModelErrorTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TModel model = default(TModel),
            ModelStateDictionary modelState = null)
            : base(controller, actionName, caughtException, modelState)
        {
            this.Model = model;
        }

        /// <summary>
        /// Gets model from invoked action in ASP.NET Web API controller.
        /// </summary>
        /// <value>Model from invoked action.</value>
        protected TModel Model { get; private set; }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingModelStateError(string errorKey)
        {
            if (!this.ModelState.ContainsKey(errorKey) || this.ModelState.Count == 0)
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    errorKey);
            }

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithError);
            this.ContainingModelStateError(memberName);

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model,
                this,
                memberName,
                this.ModelState[memberName].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        public IAndModelErrorTestBuilder<TModel> ContainingNoModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithNoError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithNoError);
            if (this.ModelState.ContainsKey(memberName))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have no model errors against key {2}, but found some.",
                    memberName);
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorTestBuilder<TModel> AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        public TModel AndProvideTheModel()
        {
            CommonValidator.CheckForEqualityWithDefaultValue(this.Model, "AndProvideTheModel can be used when there is response model from the action.");
            return this.Model;
        }

        private void ThrowNewModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    errorKey));
        }
    }
}
