﻿namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;
    using Contracts.Attributes;

    /// <summary>
    /// Base class for all attribute test builders containing common assertion methods.
    /// </summary>
    /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type for common methods.</typeparam>
    public abstract class BaseAttributesTestBuilder<TAttributesTestBuilder> : BaseAttributesTestBuilder
        where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributesTestBuilder{TAttributesBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the attributes test builder.
        /// </summary>
        /// <value>Test builder for the attributes.</value>
        public abstract TAttributesTestBuilder AttributesTestBuilder { get; }

        ///// <inheritdoc />
        public TAttributesTestBuilder ContainingAttributeOfType(Type attribute)
        {
            this.Validations.Add(attrs =>
            {
                if (attrs.All(a => a.GetType() != attribute))
                {
                    this.ThrowNewAttributeAssertionException(
                        attribute.ToFriendlyTypeName(),
                        "in fact such was not found");
                }
            });

            return this.AttributesTestBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
             where TAttribute : Attribute => this.ContainingAttributeOfType(typeof(TAttribute));
        
        /// <inheritdoc />
        public TAttributesTestBuilder PassingFor<TAttribute>(Action<TAttribute> assertions)
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>();
            this.Validations.Add(attrs => assertions(this.GetAttributeOfType<TAttribute>(attrs)));
            return this.AttributesTestBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder PassingFor<TAttribute>(Func<TAttribute, bool> predicate)
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>();
            this.Validations.Add(attrs =>
            {
                var attribute = this.GetAttributeOfType<TAttribute>(attrs);
                if (!predicate(attribute))
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{attribute.GetName()} passing the given predicate",
                        "it failed");
                }
            });

            return this.AttributesTestBuilder;
        }

        /// <summary>
        /// Gets an attribute of the given type from the provided collection of objects and throws exception if such is not found.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="attributes">Collection of attributes.</param>
        /// <returns>The found attribute of the given type.</returns>
        public TAttribute GetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute 
            => (TAttribute)attributes.First(a => a.GetType() == typeof(TAttribute));

        /// <summary>
        /// Gets an attribute of the given type from the provided collection of objects.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="attributes">Collection of attributes.</param>
        /// <returns>The found attribute of the given type or null, if such attribute is not found.</returns>
        protected TAttribute TryGetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute 
            => attributes.FirstOrDefault(a => a.GetType() == typeof(TAttribute)) as TAttribute;

        public virtual void ThrowNewAttributeAssertionException(string expectedValue, string actualValue) 
            => throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.TestContext.Component.GetName(),
                expectedValue,
                actualValue));
    }
}
