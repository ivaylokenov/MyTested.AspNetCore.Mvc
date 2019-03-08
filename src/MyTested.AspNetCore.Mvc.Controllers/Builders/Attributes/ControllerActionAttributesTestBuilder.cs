namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Contracts.Attributes;
    using Utilities.Validators;

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
            => this.ChangingRouteTo(route => route
                .WithTemplate(template)
                .WithName(withName)
                .WithOrder(withOrder ?? 0));

        /// <inheritdoc />
        public TAttributesTestBuilder ChangingRouteTo(Action<IRouteAttributeTestBuilder> routeAttributeBuilder)
        {
            this.ContainingAttributeOfType<RouteAttribute>();

            this.Validations.Add(attrs =>
            {
                var newRouteAttributeTestBuilder = new RouteAttributeTestBuilder(
                    this.TestContext, 
                    this.ThrowNewAttributeAssertionException);

                routeAttributeBuilder(newRouteAttributeTestBuilder);

                var expectedRouteAttribute = newRouteAttributeTestBuilder.GetRouteAttribute();
                var actualRouteAttribute = this.GetAttributeOfType<RouteAttribute>(attrs);

                var validations = newRouteAttributeTestBuilder.GetRouteAttributeValidations();
                validations.ForEach(v => v(expectedRouteAttribute, actualRouteAttribute));
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
        public TAttributesTestBuilder SpecifyingConsumption(string ofContentType)
        {
            this.ContainingAttributeOfType<ConsumesAttribute>();

            this.Validations.Add(attrs =>
            {
                var consumesAttribute = this.GetAttributeOfType<ConsumesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContainingOfContentType(
                    consumesAttribute,
                    ofContentType,
                    this.ThrowNewAttributeAssertionException);
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingConsumption(IEnumerable<string> ofContentTypes)
        {
            this.ContainingAttributeOfType<ConsumesAttribute>();

            this.Validations.Add(attrs =>
            {
                var consumesAttribute = this.GetAttributeOfType<ConsumesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContentTypes(
                    consumesAttribute,
                    ofContentTypes,
                    this.ThrowNewAttributeAssertionException);
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingConsumption(string ofContentType, params string[] withOtherContentTypes)
            => this.SpecifyingConsumption(new List<string>(withOtherContentTypes) { ofContentType });

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(string ofContentType)
        {
            this.ContainingAttributeOfType<ProducesAttribute>();

            this.Validations.Add(attrs =>
            {
                var producesAttribute = this.GetAttributeOfType<ProducesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContainingOfContentType(
                    producesAttribute,
                    ofContentType,
                    this.ThrowNewAttributeAssertionException);
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(IEnumerable<string> ofContentTypes)
        {
            this.ContainingAttributeOfType<ProducesAttribute>();

            this.Validations.Add(attrs =>
            {
                var consumesAttribute = this.GetAttributeOfType<ProducesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContentTypes(
                    consumesAttribute,
                    ofContentTypes,
                    this.ThrowNewAttributeAssertionException);
            });

            return this.AttributesBuilder;
        }

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(string ofContentType, params string[] withOtherContentTypes)
            => this.SpecifyingProduction(new List<string>(withOtherContentTypes) { ofContentType });

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(Type withType)
            => this.SpecifyingProduction(production => production.WithType(withType));

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(Type withType, IEnumerable<string> withContentTypes)
            => this.SpecifyingProduction(production => production
                .WithType(withType)
                .WithContentTypes(withContentTypes));

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(Type withType, params string[] withContentTypes)
            => this.SpecifyingProduction(withType, withContentTypes.AsEnumerable());

        /// <inheritdoc />
        public TAttributesTestBuilder SpecifyingProduction(Action<IProducesAttributeTestBuilder> producesAttributeBuilder)
        {
            this.ContainingAttributeOfType<ProducesAttribute>();

            this.Validations.Add(attrs =>
            {
                var newProducesAttributeTestBuilder = new ProducesAttributeTestBuilder(
                    this.TestContext,
                    this.ThrowNewAttributeAssertionException);

                producesAttributeBuilder(newProducesAttributeTestBuilder);

                var expectedProducesAttribute = newProducesAttributeTestBuilder.GetProducesAttribute();
                var actualProducesAttribute = this.GetAttributeOfType<ProducesAttribute>(attrs);

                var validations = newProducesAttributeTestBuilder.GetProducesAttributeValidations();
                validations.ForEach(v => v(expectedProducesAttribute, actualProducesAttribute));
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

        /// <inheritdoc />
        public TAttributesTestBuilder AddingFormat()
            => this.ContainingAttributeOfType<FormatFilterAttribute>();
    }
}
