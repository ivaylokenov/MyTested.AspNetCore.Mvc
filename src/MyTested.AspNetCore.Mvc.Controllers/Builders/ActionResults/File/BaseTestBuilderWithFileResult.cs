namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using Base;
    using Contracts.ActionResults.Base;
    using Contracts.Base;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base class for all test builders with file result.
    /// </summary>
    /// <typeparam name="TFileResult">Type inheriting <see cref="FileResult"/>.</typeparam>
    /// <typeparam name="TFileResultTestBuilder">Type of file result test builder to use as a return type for common methods.</typeparam>
    public abstract class BaseTestBuilderWithFileResult<TFileResult, TFileResultTestBuilder> :
        BaseTestBuilderWithActionResult<TFileResult>,
        IBaseTestBuilderWithFileResult<TFileResultTestBuilder>,
        IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>
        where TFileResult : FileResult
        where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithFileResult{TFileResult, TFileResultTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithFileResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the file result test builder.
        /// </summary>
        /// <value>Test builder for the file <see cref="ActionResult"/>.</value>
        public abstract TFileResultTestBuilder ResultTestBuilder { get; }

        /// <summary>
        /// Throws new <see cref="FileResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new FileResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "file",
                propertyName,
                expectedValue,
                actualValue));
    }
}
