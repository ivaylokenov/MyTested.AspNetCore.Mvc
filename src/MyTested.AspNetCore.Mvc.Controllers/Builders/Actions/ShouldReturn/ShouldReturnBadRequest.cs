namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.BadRequest;
    using Contracts.ActionResults.BadRequest;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder BadRequest() => this.BadRequest(null);

        /// <inheritdoc />
        public IAndTestBuilder BadRequest(Action<IBadRequestTestBuilder> badRequestTestBuilder)
        {
            if (this.ActionResult is BadRequestObjectResult)
            {
                return this.ValidateBadRequestResult<BadRequestObjectResult>(badRequestTestBuilder);
            }

            return this.ValidateBadRequestResult<BadRequestResult>(badRequestTestBuilder);
        }

        private IAndTestBuilder ValidateBadRequestResult<TBadRequestResult>(
            Action<IBadRequestTestBuilder> badRequestTestBuilder)
            where TBadRequestResult : ActionResult
            => this.ValidateActionResult<TBadRequestResult, IBadRequestTestBuilder>(
                badRequestTestBuilder,
                new BadRequestTestBuilder<TBadRequestResult>(this.TestContext));
    }
}
