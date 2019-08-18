namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Unauthorized;
    using Contracts.ActionResults.Unauthorized;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="UnauthorizedResult"/> 
    /// or <see cref="UnauthorizedObjectResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Unauthorized() => this.Unauthorized(null);
        
        /// <inheritdoc />	
        public IAndTestBuilder Unauthorized(Action<IUnauthorizedTestBuilder> unauthorizedTestBuilder)
        {
            if (this.ActionResult is UnauthorizedObjectResult)
            {
                return this.ValidateUnauthorizedResult<UnauthorizedObjectResult>(unauthorizedTestBuilder);
            }

            return this.ValidateUnauthorizedResult<UnauthorizedResult>(unauthorizedTestBuilder);
        }

        private IAndTestBuilder ValidateUnauthorizedResult<TUnauthorizedResult>(Action<IUnauthorizedTestBuilder> unauthorizedTestBuilder)
           where TUnauthorizedResult : ActionResult
           => this.ValidateActionResult<TUnauthorizedResult, IUnauthorizedTestBuilder>(
               unauthorizedTestBuilder,
               new UnauthorizedTestBuilder<TUnauthorizedResult>(this.TestContext));
    }
}
