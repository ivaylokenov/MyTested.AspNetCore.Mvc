namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Accepted;
    using Contracts.ActionResults.Accepted;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="AcceptedResult"/>,
    /// <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Accepted() => this.Accepted(null);

        /// <inheritdoc />
        public IAndTestBuilder Accepted(Action<IAcceptedTestBuilder> acceptedTestBuilder)
        {
            if (this.ActionResult is AcceptedAtActionResult)
            {
                return this.ValidateAcceptedResult<AcceptedAtActionResult>(acceptedTestBuilder);
            }

            if (this.ActionResult is AcceptedAtRouteResult)
            {
                return this.ValidateAcceptedResult<AcceptedAtRouteResult>(acceptedTestBuilder);
            }

            return this.ValidateAcceptedResult<AcceptedResult>(acceptedTestBuilder);
        }

        private IAndTestBuilder ValidateAcceptedResult<TAcceptedResult>(
            Action<IAcceptedTestBuilder> acceptedTestBuilder)
            where TAcceptedResult : ObjectResult
            => this.ValidateActionResult<TAcceptedResult, IAcceptedTestBuilder>(
                acceptedTestBuilder,
                new AcceptedTestBuilder<TAcceptedResult>(this.TestContext));
    }
}
