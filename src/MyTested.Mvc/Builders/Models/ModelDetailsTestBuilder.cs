namespace MyTested.Mvc.Builders.Models
{
    using System;
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing the model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelDetailsTestBuilder<TResponseModel>
        : ModelErrorTestBuilder<TResponseModel>, IModelDetailsTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDetailsTestBuilder{TResponseModel}"/> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        public ModelDetailsTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions)
        {
            assertions(this.Model);
            return new ModelErrorTestBuilder<TResponseModel>(this.TestContext);
        }

        /// <inheritdoc />
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
