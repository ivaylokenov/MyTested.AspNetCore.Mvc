namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Ok;
    using Contracts.ActionResults.Ok;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="OkResult"/> or <see cref="OkObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Ok() => this.Ok(null);

        /// <inheritdoc />
        public IAndTestBuilder Ok(Action<IOkTestBuilder> okTestBuilder)
        {
            if (this.ActionResult is OkObjectResult)
            {
                return this.ValidateOkResult<OkObjectResult>(okTestBuilder);
            }

            return this.ValidateOkResult<OkResult>(okTestBuilder);
        }

        private IAndTestBuilder ValidateOkResult<TOkResult>(Action<IOkTestBuilder> okTestBuilder)
            where TOkResult : ActionResult
            => this.ValidateActionResult<TOkResult, IOkTestBuilder>(
                okTestBuilder,
                new OkTestBuilder<TOkResult>(this.TestContext));
    }
}
