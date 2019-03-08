namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="RouteAttribute"/>.
    /// </summary>
    public class RouteAttributeTestBuilder : BaseTestBuilderWithComponent, IAndRouteAttributeTestBuilder
    {
        private readonly string exceptionMessagePrefix = $"{nameof(RouteAttribute)} with ";

        private readonly ICollection<Action<RouteAttribute, RouteAttribute>> validations;
        private readonly Action<string, string> failedValidationAction;

        private RouteAttribute routeAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public RouteAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(ComponentTestContext));
            CommonValidator.CheckForNullReference(failedValidationAction, nameof(failedValidationAction));

            this.failedValidationAction = failedValidationAction;

            this.routeAttribute = new RouteAttribute(string.Empty);
            this.validations = new List<Action<RouteAttribute, RouteAttribute>>();
        }

        /// <inheritdoc />
        public IAndRouteAttributeTestBuilder WithTemplate(string template)
        {
            this.routeAttribute = new RouteAttribute(template)
            {
                Name = this.routeAttribute.Name,
                Order = this.routeAttribute.Order
            };

            this.validations.Add((expected, actual) =>
            {
                var expectedTemplate = expected.Template;
                var actualTemplate = actual.Template;

                if (!string.Equals(expectedTemplate, actualTemplate, StringComparison.OrdinalIgnoreCase))
                {
                    this.failedValidationAction(
                        $"{this.exceptionMessagePrefix}'{expectedTemplate}' template",
                        $"in fact found '{actualTemplate}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRouteAttributeTestBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.routeAttribute.Name = name;
                this.validations.Add((expected, actual) =>
                {
                    var expectedName = expected.Name;
                    var actualName = actual.Name;

                    if (expectedName != actualName)
                    {
                        this.failedValidationAction(
                            $"{this.exceptionMessagePrefix}'{expectedName}' name",
                            $"in fact found '{actualName}'");
                    }
                });
            }
            
            return this;
        }

        /// <inheritdoc />
        public IAndRouteAttributeTestBuilder WithOrder(int order)
        {
            if (order != 0)
            {
                this.routeAttribute.Order = order;
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
            }
            
            return this;
        }

        /// <inheritdoc />
        public IRouteAttributeTestBuilder AndAlso() => this;

        internal RouteAttribute GetRouteAttribute()
            => this.routeAttribute;

        internal ICollection<Action<RouteAttribute, RouteAttribute>> GetRouteAttributeValidations()
            => this.validations;
    }
}
