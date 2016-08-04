namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Attributes;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Utilities.Extensions;

    using HttpMethod = System.Net.Http.HttpMethod;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public class ActionAttributesTestBuilder : BaseAttributesTestBuilder, IAndActionAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ActionAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName)
        {
            this.ContainingAttributeOfType<ActionNameAttribute>();
            this.Validations.Add(attrs =>
            {
                var actionNameAttribute = this.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;
                if (actionName != actualActionName)
                {
                    this.ThrowNewAttributeAssertionException(
                        $"{actionNameAttribute.GetName()} with '{actionName}' name",
                        $"in fact found '{actualActionName}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles);

            return this;
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder DisablingActionCall()
        {
            return this.ContainingAttributeOfType<NonActionAttribute>();
        }
        
        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
        {
            return this.RestrictingForHttpMethods(new THttpMethod().HttpMethods);
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod(string httpMethod)
        {
            return this.RestrictingForHttpMethod(new HttpMethod(httpMethod));
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod(HttpMethod httpMethod)
        {
            return this.RestrictingForHttpMethods(new List<HttpMethod> { httpMethod });
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(IEnumerable<string> httpMethods)
        {
            return this.RestrictingForHttpMethods(httpMethods.Select(m => new HttpMethod(m)));
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(params string[] httpMethods)
        {
            return this.RestrictingForHttpMethods(httpMethods.AsEnumerable());
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(IEnumerable<HttpMethod> httpMethods)
        {
            this.Validations.Add(attrs =>
            {
                var totalAllowedHttpMethods = attrs.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

                httpMethods.ForEach(httpMethod =>
                {
                    var method = httpMethod.Method;

                    if (!totalAllowedHttpMethods.Contains(method))
                    {
                        this.ThrowNewAttributeAssertionException(
                            $"attribute restricting requests for HTTP '{method}' method",
                            "in fact none was found");
                    }
                });
            });

            return this;
        }

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(params HttpMethod[] httpMethods)
        {
            return this.RestrictingForHttpMethods(httpMethods.AsEnumerable());
        }

        /// <inheritdoc />
        public IActionAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When calling {0} action in {1} expected action to have {2}, but {3}.",
                this.TestContext.MethodName,
                this.TestContext.Component.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
