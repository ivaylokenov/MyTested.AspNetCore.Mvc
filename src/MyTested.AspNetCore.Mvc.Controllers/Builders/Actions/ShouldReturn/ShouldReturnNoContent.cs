namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="NoContentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder NoContent()
            => this.ValidateActionResult<NoContentResult, IAndTestBuilder>();
    }
}
