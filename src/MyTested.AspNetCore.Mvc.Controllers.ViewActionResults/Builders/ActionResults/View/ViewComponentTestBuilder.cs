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
        : BaseTestBuilderWithViewFeatureResult<ViewComponentResult>, 
        IAndViewComponentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewComponentTestBuilder(ControllerTestContext testContext)
            : base(testContext, "view component")
        {
            // Uses internal reflection caching.
            this.ViewComponentArguments = new RouteValueDictionary(this.ActionResult.Arguments);
        }

        public IDictionary<string, object> ViewComponentArguments { get; private set; }
        
        /// <inheritdoc />
        public IViewComponentTestBuilder AndAlso() => this;
    }
}
