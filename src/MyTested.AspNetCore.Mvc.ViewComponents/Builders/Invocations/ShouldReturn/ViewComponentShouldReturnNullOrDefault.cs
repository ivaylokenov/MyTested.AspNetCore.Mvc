namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using And;
    using Contracts.And;
    using Exceptions;
    using Utilities;
    using Utilities.Validators;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder DefaultValue()
        {
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "the default value of {0}, but in fact it was not.",
                    typeof(TInvocationResult).ToFriendlyTypeName()));
            }
            
            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder Null()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TInvocationResult));
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "null, but instead received {0}.",
                    typeof(TInvocationResult).ToFriendlyTypeName()));
            }

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder NotNull()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TInvocationResult));
            if (this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "not null, but it was {0} object.",
                    typeof(TInvocationResult).ToFriendlyTypeName()));
            }

            return new AndTestBuilder(this.TestContext);
        }

        private bool CheckValidDefaultValue()
        {
            return CommonValidator.CheckForDefaultValue(this.TestContext.MethodResult) && this.CaughtException == null;
        }

        private void ThrowNewActionResultAssertionException(string message)
        {
            throw new InvocationResultAssertionException(
                $"{this.TestContext.ExceptionMessagePrefix} view component result to be {message}");
        }
    }
}
