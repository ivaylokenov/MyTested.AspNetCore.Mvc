namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for controller action test builders.
    /// </summary>
    public abstract class ControllerActionAttributesTestBuilder : BaseAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerActionAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerActionAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Tests whether the action attributes contain <see cref="RouteAttribute"/>.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        protected void ChangingRouteTo(
            string template,
            Action<string, string> failedValidationAction,
            string withName = null,
            int? withOrder = null)
        {
            this.ContainingAttributeOfType<RouteAttribute>(failedValidationAction);
            this.Validations.Add(attrs =>
            {
                var routeAttribute = this.TryGetAttributeOfType<RouteAttribute>(attrs);
                var actualTemplate = routeAttribute.Template;
                if (!string.Equals(template, actualTemplate, StringComparison.OrdinalIgnoreCase))
                {
                    failedValidationAction(
                        $"{routeAttribute.GetName()} with '{template}' template",
                        $"in fact found '{actualTemplate}'");
                }

                var actualName = routeAttribute.Name;
                if (!string.IsNullOrEmpty(withName) && withName != actualName)
                {
                    failedValidationAction(
                        $"{routeAttribute.GetName()} with '{withName}' name",
                        $"in fact found '{actualName}'");
                }

                var actualOrder = routeAttribute.Order;
                if (withOrder.HasValue && withOrder != actualOrder)
                {
                    failedValidationAction(
                        $"{routeAttribute.GetName()} with order of {withOrder}",
                        $"in fact found {actualOrder}");
                }
            });
        }

        /// <summary>
        /// Tests whether the action attributes contain <see cref="AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        protected void RestrictingForAuthorizedRequests(
            Action<string, string> failedValidationAction,
            string withAllowedRoles = null)
        {
            this.ContainingAttributeOfType<AuthorizeAttribute>(failedValidationAction);
            var testAllowedRoles = !string.IsNullOrEmpty(withAllowedRoles);
            if (testAllowedRoles)
            {
                this.Validations.Add(attrs =>
                {
                    var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                    var actualRoles = authorizeAttribute.Roles;
                    if (withAllowedRoles != actualRoles)
                    {
                        failedValidationAction(
                            $"{authorizeAttribute.GetName()} with allowed '{withAllowedRoles}' roles",
                            $"in fact found '{actualRoles}'");
                    }
                });
            }
        }

    }
}
