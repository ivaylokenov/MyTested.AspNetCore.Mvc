namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using Contracts.ActionResults.View;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="PartialViewResult"/>.
    /// </summary>
    public class PartialViewTestBuilder 
        : BaseTestBuilderWithViewFeatureResult<PartialViewResult, IAndPartialViewTestBuilder>,
        IAndPartialViewTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartialViewTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public PartialViewTestBuilder(ControllerTestContext testContext)
            : base(testContext, "partial view")
        {
        }

        /// <summary>
        /// Gets the partial view result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndPartialViewTestBuilder"/> type.</value>
        public override IAndPartialViewTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IPartialViewTestBuilder AndAlso() => this;
    }
}
