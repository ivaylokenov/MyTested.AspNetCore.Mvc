namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="ProducesAttribute"/>.
    /// </summary>
    public class ProducesAttributeTestBuilder : BaseTestBuilderWithComponent, IAndProducesAttributeTestBuilder
    {
        private const int DefaultContentTypesCount = 1;

        private readonly string exceptionMessagePrefix = $"{nameof(ProducesAttribute)} with ";

        private readonly ProducesAttribute producesAttribute;
        private readonly ICollection<Action<ProducesAttribute, ProducesAttribute>> validations;
        private readonly Action<string, string> failedValidationAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducesAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public ProducesAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction) 
            : base(testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(ComponentTestContext));

            this.failedValidationAction = failedValidationAction;

            this.producesAttribute = new ProducesAttribute(ContentType.TextPlain);
            this.validations = new List<Action<ProducesAttribute, ProducesAttribute>>();
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithContentType(string contentType)
        {
            this.producesAttribute.ContentTypes.Add(contentType);
            this.validations.Add((expected, actual) =>
            {
                if (!actual.ContentTypes.Contains(contentType))
                {
                    this.failedValidationAction(
                        $"{this.exceptionMessagePrefix}'{contentType}' content type",
                        "in fact such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithContentTypes(IEnumerable<string> contentTypes)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedContentTypes = expected.ContentTypes.Count - DefaultContentTypesCount;
                var actualContentTypes = actual.ContentTypes.Count;

                if (expectedContentTypes != actualContentTypes)
                {
                    this.failedValidationAction(
                        $"{this.exceptionMessagePrefix}{expectedContentTypes} {(expectedContentTypes != 1 ? "content types" : "content type")}",
                        $"in fact found {actualContentTypes}");
                }
            });

            contentTypes.ForEach(contentType => this.WithContentType(contentType));

            return this;
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithContentTypes(string contentType, params string[] otherContentTypes)
            => this.WithContentTypes(new List<string>(otherContentTypes) { contentType });

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithType(Type type)
        {
            this.producesAttribute.Type = type;
            this.validations.Add((expected, actual) =>
            {
                var expectedType = expected.Type;
                var actualType = actual.Type;

                if (Reflection.AreDifferentTypes(expectedType, actualType))
                {
                    this.failedValidationAction(
                        $"{this.exceptionMessagePrefix}'{expectedType.ToFriendlyTypeName()}' type",
                        $"in fact found '{actualType.ToFriendlyTypeName()}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithOrder(int order)
        {
            this.producesAttribute.Order = order;
            this.validations.Add((expected, actual) =>
            {
                var expectedOrder = expected.Order;
                var actualOrder = actual.Order;

                if (expectedOrder != actualOrder)
                {
                    this.failedValidationAction(
                        $"{this.exceptionMessagePrefix}order of {expectedOrder}",
                        $"in fact found {actualOrder}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IProducesAttributeTestBuilder AndAlso() => this;
        
        internal ProducesAttribute GetProducesAttribute()
            => this.producesAttribute;

        internal ICollection<Action<ProducesAttribute, ProducesAttribute>> GetProducesAttributeValidations()
            => this.validations;
    }
}
