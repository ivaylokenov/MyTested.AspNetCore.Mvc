namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="UnsupportedMediaTypeResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> UnsupportedMediaType()
        {
            InvocationResultValidator.ValidateInvocationResultType<UnsupportedMediaTypeResult>(this.TestContext);
            return this.NewAndTestBuilderWithActionResult();
        }
    }
}
