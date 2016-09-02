namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System;
    using And;
    using Contracts.And;
    using Contracts.Models;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing the model members.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public class ModelDetailsTestBuilder<TModel>
        : ModelErrorTestBuilder<TModel>, IAndModelDetailsTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDetailsTestBuilder{TResponseModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ActionTestContext"/> containing data about the currently executed assertion chain.</param>
        public ModelDetailsTestBuilder(ActionTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<TModel> assertions)
        {
            assertions(this.Model);
            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<TModel, bool> predicate)
        {
            if (!predicate(this.Model))
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model {2} to pass the given predicate, but it failed.",
                    this.TestContext.MethodName,
                    this.TestContext.Component.GetName(),
                    typeof(TModel).ToFriendlyTypeName()));
            }

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IModelDetailsTestBuilder<TModel> AndAlso() => this;
    }
}
