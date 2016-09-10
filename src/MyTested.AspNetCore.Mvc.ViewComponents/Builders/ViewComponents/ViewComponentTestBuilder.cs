namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System;
    using Attributes;
    using Base;
    using Contracts.Attributes;
    using Contracts.Base;
    using Contracts.ViewComponents;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing view components.
    /// </summary>
    public class ViewComponentTestBuilder : BaseTestBuilderWithViewComponent, IViewComponentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ViewComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewComponentTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponent NoAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ViewComponentAttributes,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponent Attributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ViewComponentAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponent Attributes(Action<IViewComponentAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ViewComponentAttributesTestBuilder(this.TestContext);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.ViewComponentAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to {1}, but {2}.",
                this.ViewComponent.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
