namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Attributes;
    using Contracts.And;
    using Contracts.Attributes;
    using Exceptions;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing action attributes.
    /// </content>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ActionAttributesTestBuilder(this.TestContext);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.ActionLevelAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this.NewAndTestBuilder();
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When calling {0} action in {1} expected action to {2}, but {3}.",
                this.ActionName,
                this.Component.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
