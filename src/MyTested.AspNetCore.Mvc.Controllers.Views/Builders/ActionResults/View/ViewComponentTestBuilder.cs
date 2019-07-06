namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using System.Collections.Generic;
    using Contracts.ActionResults.View;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Used for testing <see cref="ViewComponentResult"/>.
    /// </summary>
    public class ViewComponentTestBuilder 
        : BaseTestBuilderWithViewFeatureResult<ViewComponentResult, IAndViewComponentTestBuilder>, 
        IAndViewComponentTestBuilder
    {
        private IDictionary<string, object> viewComponentArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ViewComponentTestBuilder(ControllerTestContext testContext)
            : base(testContext, "view component")
        {
        }

        /// <summary>
        /// Gets the view component result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndViewComponentTestBuilder"/> type.</value>
        public override IAndViewComponentTestBuilder ResultTestBuilder => this;

        public IDictionary<string, object> ViewComponentArguments
        {
            get
            {
                if (this.viewComponentArguments == null)
                {
                    // Uses internal reflection caching.
                    this.viewComponentArguments = new RouteValueDictionary(this.ActionResult.Arguments);
                }

                return this.viewComponentArguments;
            }
        }

        /// <inheritdoc />
        public IViewComponentTestBuilder AndAlso() => this;
    }
}
