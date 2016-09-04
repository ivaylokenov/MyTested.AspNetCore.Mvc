namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using Contracts.Models;
    using Models;
    using System;
    using Utilities.Validators;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndModelDetailsTestBuilder<TInvocationResult> ResultOfType(Type returnType)
        {
            InvocationResultValidator.ValidateInvocationResultType(this.TestContext, returnType, true, true);
            return new ModelDetailsTestBuilder<TInvocationResult>(this.TestContext);
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
