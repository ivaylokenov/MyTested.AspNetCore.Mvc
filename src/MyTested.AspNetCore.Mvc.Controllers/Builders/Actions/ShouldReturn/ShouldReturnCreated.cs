namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Created;
    using Contracts.ActionResults.Created;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="CreatedResult"/>,
    /// <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Created() => this.Created(null);

        /// <inheritdoc />
        public IAndTestBuilder Created(Action<ICreatedTestBuilder> createdTestBuilder)
        {
            if (this.ObjectActionResult is CreatedAtActionResult)
            {
                return this.ValidateCreatedResult<CreatedAtActionResult>(createdTestBuilder);
            }

            if (this.ObjectActionResult is CreatedAtRouteResult)
            {
                return this.ValidateCreatedResult<CreatedAtRouteResult>(createdTestBuilder);
            }

            return this.ValidateCreatedResult<CreatedResult>(createdTestBuilder);
        }

        private IAndTestBuilder ValidateCreatedResult<TCreatedResult>(
            Action<ICreatedTestBuilder> createdTestBuilder)
            where TCreatedResult : ObjectResult
            => this.ValidateActionResult<TCreatedResult, ICreatedTestBuilder>(
                createdTestBuilder,
                new CreatedTestBuilder<TCreatedResult>(this.TestContext));
    }
}
