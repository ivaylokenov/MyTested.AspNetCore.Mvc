namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Content
{
    using Base;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ContentResult"/>.
    /// </summary>
    public class ContentTestBuilder
        : BaseTestBuilderWithActionResult<ContentResult>, 
        IAndContentTestBuilder,
        IBaseTestBuilderWithContentTypeResultInternal<IAndContentTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ContentTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the content result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndContentTestBuilder"/> type.</value>
        public IAndContentTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IContentTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="ContentResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new ContentResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "content",
                propertyName,
                expectedValue,
                actualValue));
    }
}
