namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    public class ViewComponentAttributesTestBuilder : BaseAttributesTestBuilder<IAndViewComponentAttributesTestBuilder>,
        IAndViewComponentAttributesTestBuilder
    {
        public ViewComponentAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }
        
        protected override IAndViewComponentAttributesTestBuilder AttributesTestBuilder => this;

        /// <inheritdoc />
        public IAndViewComponentAttributesTestBuilder IndicatingViewComponentExplicitly()
            => this.ContainingAttributeOfType<ViewComponentAttribute>();

        /// <inheritdoc />
        public IAndViewComponentAttributesTestBuilder ChangingViewComponentNameTo(string viewComponentName)
        {
            this.ContainingAttributeOfType<ViewComponentAttribute>();
            this.Validations.Add(attrs =>
            {
                var viewComponentAttribute = this.GetAttributeOfType<ViewComponentAttribute>(attrs);
                var actualViewComponentName = viewComponentAttribute.Name;
                if (viewComponentName != actualViewComponentName)
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{viewComponentAttribute.GetName()} with '{viewComponentName}' name",
                        $"in fact found '{actualViewComponentName}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IViewComponentAttributesTestBuilder AndAlso() => this;
    }
}
