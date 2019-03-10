namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="RouteAttribute"/>.
    /// </summary>
    public class RouteAttributeTestBuilder : BaseAttributeTestBuilderWithOrder<RouteAttribute>, 
        IAndRouteAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public RouteAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(RouteAttribute), failedValidationAction) 
            => this.Attribute = new RouteAttribute(string.Empty);

        /// <inheritdoc />
        public IAndRouteAttributeTestBuilder WithTemplate(string template)
        {
            this.Attribute = new RouteAttribute(template)
            {
                Name = this.Attribute.Name,
                Order = this.Attribute.Order
            };

            this.Validations.Add((expected, actual) =>
            {
                var expectedTemplate = expected.Template;
                var actualTemplate = actual.Template;

                if (!string.Equals(expectedTemplate, actualTemplate, StringComparison.OrdinalIgnoreCase))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedTemplate}' template",
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
                this.Attribute.Name = name;
                this.Validations.Add((expected, actual) =>
                {
                    var expectedName = expected.Name;
                    var actualName = actual.Name;

                    if (expectedName != actualName)
                    {
                        this.FailedValidationAction(
                            $"{this.ExceptionMessagePrefix}'{expectedName}' name",
                            $"in fact found '{actualName}'");
                    }
                });
            }
            
            return this;
        }

        /// <inheritdoc />
        public IAndRouteAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public IRouteAttributeTestBuilder AndAlso() => this;
    }
}
