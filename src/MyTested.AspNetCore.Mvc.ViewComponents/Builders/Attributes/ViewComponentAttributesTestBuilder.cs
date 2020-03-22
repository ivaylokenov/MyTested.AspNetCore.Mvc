namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing view component attributes.
    /// </summary>
    public class ViewComponentAttributesTestBuilder : BaseAttributesTestBuilder<IAndViewComponentAttributesTestBuilder>,
        IAndViewComponentAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ViewComponentAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }
        
        public override IAndViewComponentAttributesTestBuilder AttributesTestBuilder => this;
        
        /// <inheritdoc />
        public IViewComponentAttributesTestBuilder AndAlso() => this;

        public IAndViewComponentAttributesTestBuilder IncludingInherited()
            => throw new System.NotImplementedException();
    }
}
