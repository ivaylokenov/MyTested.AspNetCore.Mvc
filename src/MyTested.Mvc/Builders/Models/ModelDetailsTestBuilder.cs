namespace MyTested.Mvc.Builders.Models
{
    using System;
    using Contracts.Models;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Utilities;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public class ModelDetailsTestBuilder<TResponseModel>
        : ModelErrorTestBuilder<TResponseModel>, IModelDetailsTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="responseModel">Response model from invoked action.</param>
        public ModelDetailsTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TResponseModel responseModel)
            : base(controller, actionName, caughtException, responseModel)
        {
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions)
        {
            assertions(this.Model);
            return new ModelErrorTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.Model))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ModelErrorTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model);
        }
    }
}
