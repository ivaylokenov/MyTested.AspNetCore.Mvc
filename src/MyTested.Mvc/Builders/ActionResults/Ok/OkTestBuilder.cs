namespace MyTested.Mvc.Builders.ActionResults.Ok
{
    using System;
    using Internal.Extensions;
    using Exceptions;
    using Models;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing OK result.
    /// </summary>
    /// <typeparam name="TActionResult">Type of OK result - HttpOkResult or HttpOkObjectResult.</typeparam>
    public class OkTestBuilder<TActionResult>
        : BaseResponseModelTestBuilder<TActionResult>, IAndOkTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public OkTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        public IAndOkTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as HttpOkResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }
        
        /// <summary>
        /// AndAlso method for better readability when chaining ok tests.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        public IOkTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewOkResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new OkResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected ok result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
