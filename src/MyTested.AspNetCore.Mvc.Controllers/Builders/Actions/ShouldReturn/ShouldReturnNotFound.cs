namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.NotFound;
    using Contracts.ActionResults.NotFound;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder NotFound() => this.NotFound(null);

        /// <inheritdoc />
        public IAndTestBuilder NotFound(Action<INotFoundTestBuilder> notFoundTestBuilder)
        {
            if (this.ActionResult is NotFoundObjectResult)
            {
                return this.ValidateNotFoundResult<NotFoundObjectResult>(notFoundTestBuilder);
            }

            return this.ValidateNotFoundResult<NotFoundResult>(notFoundTestBuilder);
        }

        private IAndTestBuilder ValidateNotFoundResult<TNotFoundResult>(
            Action<INotFoundTestBuilder> notFoundTestBuilder)
            where TNotFoundResult : ActionResult
            => this.ValidateActionResult<TNotFoundResult, INotFoundTestBuilder>(
                notFoundTestBuilder,
                new NotFoundTestBuilder<TNotFoundResult>(this.TestContext));
    }
}
