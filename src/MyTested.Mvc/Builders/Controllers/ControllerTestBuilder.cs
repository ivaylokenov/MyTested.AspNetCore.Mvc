namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using Attributes;
    using Base;
    using Contracts.Attributes;
    using Contracts.Base;
    using Contracts.Controllers;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing controllers.
    /// </summary>
    public class ControllerTestBuilder : BaseTestBuilderWithController, IControllerTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithController NoAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithController Attributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithController Attributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ControllerAttributesTestBuilder(this.TestContext);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.ControllerLevelAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
