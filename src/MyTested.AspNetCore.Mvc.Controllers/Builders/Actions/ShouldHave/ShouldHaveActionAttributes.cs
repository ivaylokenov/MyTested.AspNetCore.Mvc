namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Attributes;
    using Contracts.Actions;
    using Contracts.Attributes;
    using Exceptions;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing action attributes.
    /// </content>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndActionResultTestBuilder<TActionResult> NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.TestContext.MethodAttributes,
                this.ThrowNewAttributeAssertionException);

            return this.Builder;
        }

        /// <inheritdoc />
        public IAndActionResultTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.TestContext.MethodAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this.Builder;
        }

        /// <inheritdoc />
        public IAndActionResultTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ActionAttributesTestBuilder(this.TestContext);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.TestContext.MethodAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this.Builder;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue) 
            => throw new AttributeAssertionException(
                $"{this.TestContext.ExceptionMessagePrefix} action to {expectedValue}, but {actualValue}.");
    }
}
