namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using Contracts.ActionResults.View;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing view result.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - <see cref="ViewResult"/> or <see cref="PartialViewResult"/>.</typeparam>
    public class ViewTestBuilder<TViewResult>
        : BaseTestBuilderWithViewFeatureResult<TViewResult>, 
        IAndViewTestBuilder,
        IBaseTestBuilderWithViewFeatureResultInternal<IAndViewTestBuilder>
        where TViewResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder{TViewResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="viewType">View type name.</param>
        public ViewTestBuilder(
            ControllerTestContext testContext,
            string viewType)
            : base(testContext, viewType)
        {
        }

        /// <summary>
        /// Gets the view result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndViewTestBuilder"/> type.</value>
        public IAndViewTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;
    }
}
