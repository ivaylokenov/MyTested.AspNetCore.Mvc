namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Exceptions;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing null or default value result.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> DefaultValue()
        {
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "the default value of {0}, but in fact it was not.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> Null()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "null, but instead received {0}.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> NotNull()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (this.CheckValidDefaultValue())
            {
                this.ThrowNewActionResultAssertionException(string.Format(
                    "not null, but it was {0} object.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        private bool CheckValidDefaultValue()
        {
            return CommonValidator.CheckForDefaultValue(this.ActionResult) && this.CaughtException == null;
        }

        private void ThrowNewActionResultAssertionException(string message)
        {
            throw new ActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}",
                    this.ActionName,
                    this.Component.GetName(),
                    message));
        }
    }
}
