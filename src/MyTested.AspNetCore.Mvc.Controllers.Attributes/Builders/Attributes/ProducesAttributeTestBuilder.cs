namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="ProducesAttribute"/>.
    /// </summary>
    public class ProducesAttributeTestBuilder : BaseAttributeTestBuilderWithOrder<ProducesAttribute>,
        IAndProducesAttributeTestBuilder
    {
        private const int DefaultContentTypesCount = 1;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducesAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public ProducesAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction) 
            : base(testContext, nameof(ProducesAttribute), failedValidationAction) 
            => this.Attribute = new ProducesAttribute(ContentType.TextPlain);

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithContentType(string contentType)
        {
            this.Attribute.ContentTypes.Add(contentType);
            this.Validations.Add((expected, actual) =>
            {
                if (!actual.ContentTypes.Contains(contentType))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{contentType}' content type",
                        "in fact such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithContentTypes(IEnumerable<string> contentTypes)
        {
            this.Validations.Add((expected, actual) =>
            {
                var expectedContentTypes = expected.ContentTypes.Count - DefaultContentTypesCount;
                var actualContentTypes = actual.ContentTypes.Count;

                if (expectedContentTypes != actualContentTypes)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}{expectedContentTypes} {(expectedContentTypes != 1 ? "content types" : "content type")}",
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
        public IAndProducesAttributeTestBuilder OfType(Type type)
        {
            this.Attribute.Type = type;
            this.Validations.Add((expected, actual) =>
            {
                var expectedType = expected.Type;
                var actualType = actual.Type;

                if (Reflection.AreDifferentTypes(expectedType, actualType))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedType.ToFriendlyTypeName()}' type",
                        $"in fact found '{actualType.ToFriendlyTypeName()}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndProducesAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public IProducesAttributeTestBuilder AndAlso() => this;
    }
}
