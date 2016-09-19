namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

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
        public IAndControllerAttributesTestBuilder IndicatingControllerExplicitly()
            => this.ContainingAttributeOfType<ControllerAttribute>();

        /// <inheritdoc />
        public IControllerAttributesTestBuilder AndAlso() => this;

        protected override IAndControllerAttributesTestBuilder GetAttributesTestBuilder() => this;
    }
}
