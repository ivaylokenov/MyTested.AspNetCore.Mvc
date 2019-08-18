namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.LocalRedirect
{
    using Builders.Base;
    using Contracts.ActionResults.LocalRedirect;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="LocalRedirectResult"/>.
    /// </summary>
    public class LocalRedirectTestBuilder 
        : BaseTestBuilderWithActionResult<LocalRedirectResult>,
        IAndLocalRedirectTestBuilder,
        IBaseTestBuilderWithRedirectResultInternal<IAndLocalRedirectTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalRedirectTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public LocalRedirectTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the local redirect result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndLocalRedirectTestBuilder"/> type.</value>
        public IAndLocalRedirectTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public ILocalRedirectTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="RedirectResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new RedirectResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "local redirect",
                propertyName,
                expectedValue,
                actualValue));
    }
}
