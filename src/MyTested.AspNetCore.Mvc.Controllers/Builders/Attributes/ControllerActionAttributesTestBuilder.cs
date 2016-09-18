namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Contracts.Attributes;

    /// <summary>
    /// Base class for controller action test builders.
    /// </summary>
    public abstract class ControllerActionAttributesTestBuilder<TAttributesTestBuilder> : BaseAttributesTestBuilder<TAttributesTestBuilder>
        where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerActionAttributesTestBuilder{TBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected ControllerActionAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public TAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ContainingAttributeOfType<RouteAttribute>();
            this.Validations.Add(attrs =>
            {
                var routeAttribute = this.TryGetAttributeOfType<RouteAttribute>(attrs);
                var actualTemplate = routeAttribute.Template;
                if (!string.Equals(template, actualTemplate, StringComparison.OrdinalIgnoreCase))
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{routeAttribute.GetName()} with '{template}' template",
                        $"in fact found '{actualTemplate}'");
                }

                var actualName = routeAttribute.Name;
                if (!string.IsNullOrEmpty(withName) && withName != actualName)
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{routeAttribute.GetName()} with '{withName}' name",
                        $"in fact found '{actualName}'");
                }

                var actualOrder = routeAttribute.Order;
                if (withOrder.HasValue && withOrder != actualOrder)
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{routeAttribute.GetName()} with order of {withOrder}",
                        $"in fact found {actualOrder}");
                }
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingArea(string areaName)
        {
            this.ContainingAttributeOfType<AreaAttribute>();
            this.Validations.Add(attrs =>
            {
                var areaAttribute = this.GetAttributeOfType<AreaAttribute>(attrs);
                var actualAreaName = areaAttribute.RouteValue;
                if (areaName != actualAreaName)
                {
                    this.ThrowNewAttributeAssertionException(
                        $"'{areaName}' area",
                        $"in fact found '{actualAreaName}'");
                }
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder RestrictingForAuthorizedRequests(string withAllowedRoles = null)
        {
            this.ContainingAttributeOfType<AuthorizeAttribute>();
            var testAllowedRoles = !string.IsNullOrEmpty(withAllowedRoles);
            if (testAllowedRoles)
            {
                this.Validations.Add(attrs =>
                {
                    var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                    var actualRoles = authorizeAttribute.Roles;
                    if (withAllowedRoles != actualRoles)
                    {
                        this.ThrowNewAttributeAssertionException(
                            $"{authorizeAttribute.GetName()} with allowed '{withAllowedRoles}' roles",
                            $"in fact found '{actualRoles}'");
                    }
                });
            }

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder AllowingAnonymousRequests()
            => this.ContainingAttributeOfType<AllowAnonymousAttribute>();
    }
}
