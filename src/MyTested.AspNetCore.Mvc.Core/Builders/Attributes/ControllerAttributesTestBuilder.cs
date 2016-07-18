namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public class ControllerAttributesTestBuilder : CoreAttributesTestBuilder, IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndControllerAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <inheritdoc />
        public IAndControllerAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        /// <inheritdoc />
        public IAndControllerAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <inheritdoc />
        public IAndControllerAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles);
            
            return this;
        }

        /// <inheritdoc />
        public IControllerAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
