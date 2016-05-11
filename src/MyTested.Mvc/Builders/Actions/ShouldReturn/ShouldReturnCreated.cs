namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Created;
    using Contracts.ActionResults.Created;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public ICreatedTestBuilder Created()
        {
            if (this.ActionResult is CreatedAtActionResult)
            {
                return this.ReturnCreatedTestBuilder<CreatedAtActionResult>();
            }

            if (this.ActionResult is CreatedAtRouteResult)
            {
                return this.ReturnCreatedTestBuilder<CreatedAtRouteResult>();
            }

            return this.ReturnCreatedTestBuilder<CreatedResult>();
        }

        private ICreatedTestBuilder ReturnCreatedTestBuilder<TCreatedResult>()
            where TCreatedResult : ObjectResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TCreatedResult>();
            return new CreatedTestBuilder<TCreatedResult>(this.TestContext);
        }
    }
}
