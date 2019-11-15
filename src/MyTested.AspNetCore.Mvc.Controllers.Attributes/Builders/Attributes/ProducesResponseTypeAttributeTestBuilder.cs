namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;

    /// <summary>
    /// Used for testing <see cref="ProducesResponseTypeAttribute"/>.
    /// </summary>
    public class ProducesResponseTypeAttributeTestBuilder : BaseAttributeTestBuilder<ProducesResponseTypeAttribute>, 
        IAndProducesResponseTypeAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducesResponseTypeAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public ProducesResponseTypeAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(ProducesResponseTypeAttribute), failedValidationAction)
            => this.Attribute = new ProducesResponseTypeAttribute(HttpStatusCode.OK);

        /// <inheritdoc />
        public IAndProducesResponseTypeAttributeTestBuilder OfType(Type type)
        {
            this.Attribute.Type = type;
            this.Validations.Add((expected, actual) =>
            {
                var expectedType = expected.Type;
                var actualType = actual.Type;

                if (Reflection.AreDifferentTypes(expected, actual))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedType.ToFriendlyTypeName()}' type",
                        $"in fact found '{actualType.ToFriendlyTypeName()}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndProducesResponseTypeAttributeTestBuilder WithStatusCode(int statusCode)
        {
            this.Attribute.StatusCode = statusCode;
            this.Validations.Add((expected, actual) =>
            {
                var expectedStatusCode = expected.StatusCode;
                var actualStatusCode = actual.StatusCode;

                if (expectedStatusCode != actualStatusCode)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedStatusCode}' status code",
                        $"in fact found '{actualStatusCode}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IProducesResponseTypeAttributeTestBuilder AndAlso() => this;
    }
}
