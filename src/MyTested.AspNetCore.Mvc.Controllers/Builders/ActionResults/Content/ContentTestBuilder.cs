namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Content
{
    using Builders.Base;
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

        public IAndContentTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IContentTestBuilder AndAlso() => this;
        
        public void ThrowNewContentResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new ContentResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "content",
                propertyName,
                expectedValue,
                actualValue));
    }
}
