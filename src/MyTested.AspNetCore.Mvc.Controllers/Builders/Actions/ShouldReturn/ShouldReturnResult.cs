namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using Contracts.Models;
    using Exceptions;
    using Models;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing return type.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType)
        {
            InvocationResultValidator.ValidateInvocationReturnType(this.TestContext, returnType, true, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>()
        {
            InvocationResultValidator.ValidateInvocationReturnType<TResponseModel>(this.TestContext, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> Result<TResponseModel>(TResponseModel model)
        {
            InvocationResultValidator.ValidateInvocationReturnType<TResponseModel>(this.TestContext);

            if (Reflection.AreNotDeeplyEqual(model, this.ActionResult))
            {
                throw new ResponseModelAssertionException($"When calling {this.ActionName} action in {this.Controller.GetName()} expected the response model to be the given model, but in fact it was a different one.");
            }

            this.TestContext.Model = model;
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        public TReturnObject GetReturnObject<TReturnObject>()
            where TReturnObject : class
        {
            InvocationResultValidator.ValidateInvocationReturnType<TReturnObject>(this.TestContext);
            return this.ActionResult as TReturnObject;
        }
    }
}
