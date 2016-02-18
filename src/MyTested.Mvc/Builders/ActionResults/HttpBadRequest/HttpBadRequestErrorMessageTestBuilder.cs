namespace MyTested.Mvc.Builders.ActionResults.HttpBadRequest
{
    using Base;
    using Contracts.ActionResults.HttpBadRequest;
    using Exceptions;
    using Utilities.Extensions;
    using Internal.TestContexts;
    /// <summary>
    /// Used for testing specific bad request text error messages.
    /// </summary>
    public class HttpBadRequestErrorMessageTestBuilder
        : BaseTestBuilderWithInvokedAction, IHttpBadRequestErrorMessageTestBuilder
    {
        private readonly string actualMessage;
        private readonly IAndHttpBadRequestTestBuilder httpBadRequestTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpBadRequestErrorMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actualMessage">Actual text error message received from bad request result.</param>
        /// <param name="httpBadRequestTestBuilder">HTTP bad request test builder.</param>
        public HttpBadRequestErrorMessageTestBuilder(
            ControllerTestContext testContext,
            string actualMessage,
            IAndHttpBadRequestTestBuilder httpBadRequestTestBuilder)
            : base(testContext)
        {
            this.actualMessage = actualMessage;
            this.httpBadRequestTestBuilder = httpBadRequestTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>HTTP bad request test builder.</returns>
        public IAndHttpBadRequestTestBuilder ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }

            return this.httpBadRequestTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        public IAndHttpBadRequestTestBuilder BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }

            return this.httpBadRequestTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        public IAndHttpBadRequestTestBuilder EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }

            return this.httpBadRequestTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        public IAndHttpBadRequestTestBuilder Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.httpBadRequestTestBuilder;
        }

        private void ThrowNewBadRequestResultAssertionException(string messageFormat, string operation)
        {
            throw new HttpBadRequestResultAssertionException(string.Format(
                messageFormat,
                this.ActionName,
                this.Controller.GetName(),
                operation,
                this.actualMessage));
        }
    }
}
