namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.LocalRedirect;
    using Contracts.ActionResults.LocalRedirect;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="LocalRedirectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder LocalRedirect() => this.LocalRedirect(null);

        /// <inheritdoc />
        public IAndTestBuilder LocalRedirect(Action<ILocalRedirectTestBuilder> localRedirectTestBuilder)
            => this.ValidateActionResult<LocalRedirectResult, ILocalRedirectTestBuilder>(
                localRedirectTestBuilder,
                new LocalRedirectTestBuilder(this.TestContext));
    }
}
