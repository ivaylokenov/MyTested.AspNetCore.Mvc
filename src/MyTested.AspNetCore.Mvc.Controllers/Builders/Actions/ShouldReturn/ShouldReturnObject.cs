namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Object;
    using Contracts.ActionResults.Object;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Object() => this.Object(null);

        /// <inheritdoc />
        public IAndTestBuilder Object(Action<IObjectTestBuilder> objectTestBuilder)
            => this.ValidateActionResult<ObjectResult, IObjectTestBuilder>(
                objectTestBuilder,
                new ObjectTestBuilder(this.TestContext));

    }
}
