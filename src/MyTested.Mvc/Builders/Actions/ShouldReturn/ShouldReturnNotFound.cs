namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.HttpNotFound;
    using Contracts.ActionResults.NotFound;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public INotFoundTestBuilder NotFound()
        {
            if (this.ActionResult is NotFoundObjectResult)
            {
                return this.ReturnNotFoundTestBuilder<NotFoundObjectResult>();
            }

            return this.ReturnNotFoundTestBuilder<NotFoundResult>();
        }

        private INotFoundTestBuilder ReturnNotFoundTestBuilder<TNotFoundResult>()
            where TNotFoundResult : ActionResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TNotFoundResult>();
            return new NotFoundTestBuilder<TNotFoundResult>(this.TestContext);
        }
    }
}
