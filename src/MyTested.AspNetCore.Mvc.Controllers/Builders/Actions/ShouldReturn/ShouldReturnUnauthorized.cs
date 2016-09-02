namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="UnauthorizedResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> Unauthorized()
        {
            InvocationResultValidator.ValidateInvocationReturnType<UnauthorizedResult>(this.TestContext);
            return this.NewAndTestBuilderWithActionResult();
        }
    }
}
