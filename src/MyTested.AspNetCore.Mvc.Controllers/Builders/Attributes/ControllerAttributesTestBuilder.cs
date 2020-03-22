namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public class ControllerAttributesTestBuilder 
        : BaseAttributesTestBuilder<IAndControllerAttributesTestBuilder>, IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the attributes test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndControllerAttributesTestBuilder"/>.</value>
        public override IAndControllerAttributesTestBuilder AttributesTestBuilder => this;
        
        /// <inheritdoc />
        public IControllerAttributesTestBuilder AndAlso() => this;

        /// <inheritdoc />
        public IAndControllerAttributesTestBuilder IncludingInherited()
        {
            this.TestContext.IncludeInheritedComponentAttributes();
            return this;
        }
    }
}
