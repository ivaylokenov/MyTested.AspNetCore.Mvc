namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using Contracts.Models;
    using Models;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing return type.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType)
        {
            InvocationResultValidator.ValidateInvocationResultType(this.TestContext, returnType, true, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>()
        {
            InvocationResultValidator.ValidateInvocationResultType<TResponseModel>(this.TestContext, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TActionResult> Result<TResponseModel>(TResponseModel model)
        {
            InvocationResultValidator.ValidateInvocationResult(this.TestContext, model);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }
    }
}
