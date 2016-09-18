namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public class ControllerAttributesTestBuilder : ControllerActionAttributesTestBuilder<IAndControllerAttributesTestBuilder>,
        IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IControllerAttributesTestBuilder AndAlso() => this;

        protected override IAndControllerAttributesTestBuilder GetAttributesTestBuilder() => this;
    }
}
