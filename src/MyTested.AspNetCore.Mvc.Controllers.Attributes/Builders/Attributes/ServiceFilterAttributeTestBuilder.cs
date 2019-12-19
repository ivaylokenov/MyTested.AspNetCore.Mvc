﻿namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Contracts.Attributes;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing <see cref="ServiceFilterAttribute"/>.
    /// </summary>
    public class ServiceFilterAttributeTestBuilder : BaseAttributeTestBuilderWithOrderAndType<ServiceFilterAttribute>,
        IAndServiceFilterAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFilterAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public ServiceFilterAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(ServiceFilterAttribute), failedValidationAction)
                => this.Attribute = new ServiceFilterAttribute(typeof(object));

        /// <inheritdoc />
        public IAndServiceFilterAttributeTestBuilder OfType(Type type)
        {
            this.ValidateType(type, attr => attr.ServiceType);
            return this;
        }

        /// <inheritdoc />
        public IAndServiceFilterAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public IServiceFilterAttributeTestBuilder AndAlso() => this;
    }
}
