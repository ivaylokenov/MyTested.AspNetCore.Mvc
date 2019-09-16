namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Utilities;

    /// <summary>
    /// Used for testing <see cref="MiddlewareFilterAttribute"/>.
    /// </summary>
    public class MiddlewareFilterAttributeTestBuilder : BaseAttributeTestBuilderWithOrderAndType<MiddlewareFilterAttribute>,
        IAndMiddlewareFilterAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewareFilterAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public MiddlewareFilterAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(MiddlewareFilterAttribute), failedValidationAction)
        => this.Attribute = new MiddlewareFilterAttribute(typeof(object));

        /// <inheritdoc />
        public IAndMiddlewareFilterAttributeTestBuilder OfType(Type configurationType)
        {
            this.ValidateType(configurationType, attr => attr.ConfigurationType);
            return this;
        }

        /// <inheritdoc />
        public IAndMiddlewareFilterAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public IMiddlewareFilterAttributeTestBuilder AndAlso() => this;
    }
}
