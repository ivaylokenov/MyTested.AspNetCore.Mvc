namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using And;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="EmptyResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Empty()
        {
            InvocationResultValidator.ValidateInvocationResultType<EmptyResult>(this.TestContext);
            return new AndTestBuilder(this.TestContext);
        }
    }
}
