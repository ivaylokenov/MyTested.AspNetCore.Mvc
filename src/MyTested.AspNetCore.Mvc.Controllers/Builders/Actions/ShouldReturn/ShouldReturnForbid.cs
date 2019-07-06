namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Authentication;
    using Contracts.ActionResults.Authentication;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ForbidResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Forbid() => this.Forbid(null);

        /// <inheritdoc />
        public IAndTestBuilder Forbid(Action<IForbidTestBuilder> forbidTestBuilder)
            => this.ValidateActionResult<ForbidResult, IForbidTestBuilder>(
                forbidTestBuilder,
                new ForbidTestBuilder(this.TestContext));
    }
}
