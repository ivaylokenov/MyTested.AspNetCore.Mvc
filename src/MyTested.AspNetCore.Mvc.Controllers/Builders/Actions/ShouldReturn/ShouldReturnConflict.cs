namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Conflict;
    using Contracts.ActionResults.Conflict;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ConflictResult"/> or <see cref="ConflictObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Conflict() => this.Conflict(null);

        /// <inheritdoc />
        public IAndTestBuilder Conflict(Action<IConflictTestBuilder> conflictTestBuilder)
        {
            if (this.ActionResult is ConflictObjectResult)
            {
                return this.ValidateConflictResult<ConflictObjectResult>(conflictTestBuilder);
            }

            return this.ValidateConflictResult<ConflictResult>(conflictTestBuilder);
        }

        private IAndTestBuilder ValidateConflictResult<TConflictResult>(Action<IConflictTestBuilder> conflictTestBuilder)
            where TConflictResult : ActionResult
            => this.ValidateActionResult<TConflictResult, IConflictTestBuilder>(
                conflictTestBuilder,
                new ConflictTestBuilder<TConflictResult>(this.TestContext));
    }
}
