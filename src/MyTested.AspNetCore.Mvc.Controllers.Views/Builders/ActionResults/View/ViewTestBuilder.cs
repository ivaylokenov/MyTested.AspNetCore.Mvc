namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using Contracts.ActionResults.View;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ViewResult"/>.
    /// </summary>
    public class ViewTestBuilder
        : BaseTestBuilderWithViewFeatureResult<ViewResult, IAndViewTestBuilder>, 
        IAndViewTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ViewTestBuilder(ControllerTestContext testContext)
            : base(testContext, "view")
        {
        }

        /// <summary>
        /// Gets the view result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndViewTestBuilder"/> type.</value>
        public override IAndViewTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;
    }
}
