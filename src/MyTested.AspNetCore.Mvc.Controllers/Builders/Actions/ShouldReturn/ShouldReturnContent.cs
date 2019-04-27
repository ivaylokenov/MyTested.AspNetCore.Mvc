namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Content;
    using Contracts.ActionResults.Content;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ContentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Content() => this.Content(null);

        /// <inheritdoc />
        public IAndTestBuilder Content(Action<IContentTestBuilder> contentTestBuilder)
            => this.ValidateActionResult<ContentResult, IContentTestBuilder>(
                contentTestBuilder,
                new ContentTestBuilder(this.TestContext));
    }
}
