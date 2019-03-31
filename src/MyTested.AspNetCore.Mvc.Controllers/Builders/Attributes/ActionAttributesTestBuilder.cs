namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Exceptions;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public class ActionAttributesTestBuilder
        : BaseAttributesTestBuilder<IAndActionAttributesTestBuilder>, IAndActionAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ActionAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the attributes test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndActionAttributesTestBuilder"/>.</value>
        public override IAndActionAttributesTestBuilder AttributesTestBuilder => this;
        
        /// <inheritdoc />
        public IActionAttributesTestBuilder AndAlso() => this;

        public override void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "{0} action to have {1}, but {2}.",
                this.TestContext.ExceptionMessagePrefix,
                expectedValue,
                actualValue));
        }
    }
}
