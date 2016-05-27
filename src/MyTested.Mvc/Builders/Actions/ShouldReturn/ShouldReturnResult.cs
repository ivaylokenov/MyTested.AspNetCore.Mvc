namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using Contracts.Models;
    using Exceptions;
    using Models;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Class containing methods for testing return type.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>()
        {
            this.ValidateActionReturnType<TResponseModel>(true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IModelDetailsTestBuilder<TActionResult> Result<TResponseModel>(TResponseModel model)
        {
            this.ValidateActionReturnType<TResponseModel>();

            if (Reflection.AreNotDeeplyEqual(model, this.ActionResult))
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected the response model to be the given model, but in fact it was a different one.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            this.TestContext.Model = model;
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        private TReturnObject GetReturnObject<TReturnObject>()
            where TReturnObject : class
        {
            this.ValidateActionReturnType<TReturnObject>();
            return this.ActionResult as TReturnObject;
        }
    }
}
