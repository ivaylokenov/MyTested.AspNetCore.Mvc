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
        public IAndModelDetailsTestBuilder<TResult> ResultOfType<TResult>()
        {
            InvocationResultValidator.ValidateInvocationResultType<TResult>(this.TestContext, true);
            return new ModelDetailsTestBuilder<TResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TResult> Result<TResult>(TResult model)
        {
            InvocationResultValidator.ValidateInvocationResult(this.TestContext, model);
            return new ModelDetailsTestBuilder<TResult>(this.TestContext);
        }
    }
}
