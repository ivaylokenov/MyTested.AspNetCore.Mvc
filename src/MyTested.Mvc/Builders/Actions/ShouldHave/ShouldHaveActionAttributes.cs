namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Attributes;
    using Contracts.And;
    using Contracts.Attributes;
    using Exceptions;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing action attributes and model state.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Checks whether the tested action has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Checks whether the tested action has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested action.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Checks whether the tested action has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the action.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ActionAttributesTestBuilder(this.Controller, this.ActionName);
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
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
