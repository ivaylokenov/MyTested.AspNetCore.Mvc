namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Internal.Extensions;
    using Contracts.Base;
    using Exceptions;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing null or default value result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> DefaultValue()
        {
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "the default value of {0}, but in fact it was not.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Null()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "null, but instead received {0}.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is not null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotNull()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "not null, but it was {0} object.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        private bool CheckValidDefaultValue()
        {
            return CommonValidator.CheckForDefaultValue(this.ActionResult) && this.CaughtException == null;
        }

        private void ThrowNewHttpActionResultAssertionException(string message)
        {
            throw new ActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}",
                    this.ActionName,
                    this.Controller.GetName(),
                    message));
        }
    }
}
