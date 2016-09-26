namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Object;
    using Contracts.ActionResults.Object;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="ObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndObjectTestBuilder Object()
        {
            InvocationResultValidator.ValidateInvocationResultType<ObjectResult>(this.TestContext);
            return new ObjectTestBuilder(this.TestContext);
        }
    }
}
