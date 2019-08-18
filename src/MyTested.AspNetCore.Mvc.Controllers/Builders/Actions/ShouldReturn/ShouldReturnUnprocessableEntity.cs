namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.BadRequest;
    using ActionResults.UnprocessableEntity;
    using Contracts.ActionResults.BadRequest;
    using Contracts.ActionResults.UnprocessableEntity;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="UnprocessableEntityResult"/>
    /// or <see cref="UnprocessableEntityObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder UnprocessableEntity() => this.UnprocessableEntity(null);

        /// <inheritdoc />
        public IAndTestBuilder UnprocessableEntity(Action<IUnprocessableEntityTestBuilder> unprocessableEntityTestBuilder)
        {
            if (this.ActionResult is UnprocessableEntityObjectResult)
            {
                return this.ValidateUnprocessableEntityResult<UnprocessableEntityObjectResult>(unprocessableEntityTestBuilder);
            }

            return this.ValidateUnprocessableEntityResult<UnprocessableEntityResult>(unprocessableEntityTestBuilder);
        }

        private IAndTestBuilder ValidateUnprocessableEntityResult<TUnprocessableEntityResult>(
            Action<IUnprocessableEntityTestBuilder> unprocessableEntityTestBuilder)
            where TUnprocessableEntityResult : ActionResult
            => this.ValidateActionResult<TUnprocessableEntityResult, IUnprocessableEntityTestBuilder>(
                unprocessableEntityTestBuilder,
                new UnprocessableEntityTestBuilder<TUnprocessableEntityResult>(this.TestContext));
    }
}
