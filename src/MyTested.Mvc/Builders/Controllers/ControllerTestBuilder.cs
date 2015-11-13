namespace MyTested.Mvc.Builders.Controllers
{
    using Base;
    using Contracts.Controllers;
    using Contracts.Base;
    using Microsoft.AspNet.Mvc;
    using System.Collections.Generic;
    using Utilities.Validators;
    using Common.Extensions;
    using Exceptions;
    using Contracts.Attributes;
    using System;
    using Attributes;

    /// <summary>
    /// Used for testing controllers.
    /// </summary>
    public class ControllerTestBuilder : BaseTestBuilder, IControllerTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller which will be tested.</param>
        /// <param name="controllerAttributes">Collected attributes from the tested controller.</param>
        public ControllerTestBuilder(
            Controller controller,
            IEnumerable<object> controllerAttributes)
            : base(controller, controllerAttributes)
        {
        }

        /// <summary>
        /// Checks whether the tested controller has no attributes of any type. 
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder NoAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        /// <summary>
        /// Checks whether the tested controller has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested controller.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder Attributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this;
        }

        /// <summary>
        /// Checks whether the tested controller has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the controller.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder Attributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ControllerAttributesTestBuilder(this.Controller);
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
