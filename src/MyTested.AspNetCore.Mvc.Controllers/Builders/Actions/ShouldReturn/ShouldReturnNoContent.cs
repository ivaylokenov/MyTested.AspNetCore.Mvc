namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="NoContentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> NoContent()
        {
            InvocationResultValidator.ValidateInvocationResultType<NoContentResult>(this.TestContext);
            return this.NewAndTestBuilderWithActionResult();
        }
    }
}
