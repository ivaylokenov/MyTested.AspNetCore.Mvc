namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldHave
{
    using System;
    using Attributes;
    using Contracts.Attributes;
    using Contracts.Invocations;
    using Exceptions;
    using Utilities.Validators;

    public partial class ViewComponentShouldHaveTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndViewComponentResultTestBuilder<TInvocationResult> NoAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.TestContext.ComponentAttributes,
                this.ThrowNewAttributeAssertionException);

            return this.Builder;
        }

        /// <inheritdoc />
        public IAndViewComponentResultTestBuilder<TInvocationResult> Attributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.TestContext.ComponentAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this.Builder;
        }

        /// <inheritdoc />
        public IAndViewComponentResultTestBuilder<TInvocationResult> Attributes(
            Action<IViewComponentAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ViewComponentAttributesTestBuilder(this.TestContext);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.TestContext.ComponentAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this.Builder;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(
                $"{this.TestContext.ExceptionMessagePrefix} action to {expectedValue}, but {actualValue}.");
        }
    }
}
