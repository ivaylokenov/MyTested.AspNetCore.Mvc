namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    public class ViewComponentAttributesTestBuilder : BaseAttributesTestBuilder, IAndViewComponentAttributesTestBuilder
    {
        public ViewComponentAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndViewComponentAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

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
