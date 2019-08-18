namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="UnauthorizedResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Unauthorized()
            => this.ValidateActionResult<UnauthorizedResult, IAndTestBuilder>();
    }
}
