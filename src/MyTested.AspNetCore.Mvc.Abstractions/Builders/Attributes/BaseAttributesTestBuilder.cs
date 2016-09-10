namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Internal.TestContexts;
    using Utilities;
    using Exceptions;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for all attribute test builders.
    /// </summary>
    public abstract class BaseAttributesTestBuilder : BaseTestBuilderWithComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.Validations = new List<Action<IEnumerable<object>>>();
        }

        /// <summary>
        /// Gets the validation actions for the tested attributes.
        /// </summary>
        /// <value>Collection of validation actions for the attributes.</value>
        protected ICollection<Action<IEnumerable<object>>> Validations { get; private set; }

        internal ICollection<Action<IEnumerable<object>>> GetAttributeValidations()
        {
            return this.Validations;
        }

        /// <summary>
        /// Tests whether the attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        protected void ContainingAttributeOfType<TAttribute>(Action<string, string> failedValidationAction)
            where TAttribute : Attribute
        {
            var expectedAttributeType = typeof(TAttribute);
            this.Validations.Add(attrs =>
            {
                if (attrs.All(a => a.GetType() != expectedAttributeType))
                {
                    failedValidationAction(
                        expectedAttributeType.ToFriendlyTypeName(),
                        "in fact such was not found");
                }
            });
        }

        /// <summary>
        /// Gets an attribute of the given type from the provided collection of objects and throws exception if such is not found.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="attributes">Collection of attributes.</param>
        /// <returns>The found attribute of the given type.</returns>
        protected TAttribute GetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return (TAttribute)attributes.First(a => a.GetType() == typeof(TAttribute));
        }

        /// <summary>
        /// Gets an attribute of the given type from the provided collection of objects.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="attributes">Collection of attributes.</param>
        /// <returns>The found attribute of the given type or null, if such attribute is not found.</returns>
        protected TAttribute TryGetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return attributes.FirstOrDefault(a => a.GetType() == typeof(TAttribute)) as TAttribute;
        }
        
        protected virtual void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.TestContext.Component.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
