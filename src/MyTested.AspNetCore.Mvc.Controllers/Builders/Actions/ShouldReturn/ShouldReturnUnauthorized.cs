namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using And;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="UnauthorizedResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Unauthorized()
        {
            InvocationResultValidator.ValidateInvocationResultType<UnauthorizedResult>(this.TestContext);
            return new AndTestBuilder(this.TestContext);
        }
    }
}
