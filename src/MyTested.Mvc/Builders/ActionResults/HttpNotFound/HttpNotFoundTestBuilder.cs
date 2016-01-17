namespace MyTested.Mvc.Builders.ActionResults.HttpNotFound
{
    using System;
    using Contracts.ActionResults.HttpNotFound;
    using Contracts.Base;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Models;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    /// <typeparam name="THttpNotFoundResult">Type of not found result - HttpNotFoundResult or HttpNotFoundObjectResult.</typeparam>
    public class HttpNotFoundTestBuilder<THttpNotFoundResult>
        : BaseResponseModelTestBuilder<THttpNotFoundResult>, IHttpNotFoundTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="httpNotFoundResult">Result from the tested action.</param>
        public HttpNotFoundTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            THttpNotFoundResult httpNotFoundResult)
            : base(controller, actionName, caughtException, httpNotFoundResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder with action.</returns>
        public IBaseTestBuilderWithCaughtException WithNoResponseModel()
        {
            var actualResult = this.ActionResult as HttpNotFoundResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }
    }
}
