namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Internal.TestContexts;

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
        public IViewComponentAttributesTestBuilder AndAlso() => this;
    }
}
