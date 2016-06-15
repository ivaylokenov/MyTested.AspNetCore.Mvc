namespace MyTested.AspNetCore.Mvc.Builders.Models
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
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ModelDetailsTestBuilder<TModel>
        : ModelErrorTestBuilder<TModel>, IAndModelDetailsTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDetailsTestBuilder{TResponseModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ModelDetailsTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> Passing(Action<TModel> assertions)
        {
            assertions(this.Model);
            return new ModelErrorTestBuilder<TModel>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelErrorTestBuilder<TModel> Passing(Func<TModel, bool> predicate)
        {
            if (!predicate(this.Model))
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TModel).ToFriendlyTypeName()));
            }

            return new ModelErrorTestBuilder<TModel>(this.TestContext);
        }

        /// <inheritdoc />
        public new IModelDetailsTestBuilder<TModel> AndAlso() => this;
    }
}
