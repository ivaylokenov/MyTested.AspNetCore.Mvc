namespace MyTested.Mvc.Builders.Models
{
    using System;
    using Contracts.Models;
    using Exceptions;
    using Utilities.Extensions;
    using Utilities;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET MVC controller.</typeparam>
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
        public ModelDetailsTestBuilder(ControllerTestContext testContext)
            : base(testContext)
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
            return new ModelErrorTestBuilder<TResponseModel>(this.TestContext);
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

            return new ModelErrorTestBuilder<TResponseModel>(this.TestContext);
        }
    }
}
