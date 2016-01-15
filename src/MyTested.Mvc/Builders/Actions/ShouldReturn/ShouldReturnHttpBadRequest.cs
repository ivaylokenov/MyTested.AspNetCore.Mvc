namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.HttpBadRequest;
    using Contracts.ActionResults.HttpBadRequest;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing BadRequestResult or BadRequestObjectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is BadRequestResult or BadRequestObjectResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        public IHttpBadRequestTestBuilder HttpBadRequest()
        {
            if (this.ActionResult is BadRequestObjectResult)
            {
                return this.ReturnBadRequestTestBuilder<BadRequestObjectResult>();
            }

            return this.ReturnBadRequestTestBuilder<BadRequestResult>();
        }

        private IHttpBadRequestTestBuilder ReturnBadRequestTestBuilder<TBadRequestResult>()
            where TBadRequestResult : class
        {
            var badRequestResult = this.GetReturnObject<TBadRequestResult>();
            return new HttpBadRequestTestBuilder<TBadRequestResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                badRequestResult);
        }
    }
}
