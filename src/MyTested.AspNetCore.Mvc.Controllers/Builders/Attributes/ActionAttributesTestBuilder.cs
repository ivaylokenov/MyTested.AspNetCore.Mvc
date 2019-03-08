namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Attributes;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Utilities.Extensions;

    using HttpMethod = System.Net.Http.HttpMethod;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public class ActionAttributesTestBuilder : ControllerActionAttributesTestBuilder<IAndActionAttributesTestBuilder>,
        IAndActionAttributesTestBuilder
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
        public IAndActionAttributesTestBuilder DisablingActionCall()
            => this.ContainingAttributeOfType<NonActionAttribute>();
        
        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
            => this.RestrictingForHttpMethods(new THttpMethod().HttpMethods);

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod(string httpMethod)
            => this.RestrictingForHttpMethod(new HttpMethod(httpMethod));

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethod(HttpMethod httpMethod)
            => this.RestrictingForHttpMethods(new List<HttpMethod> { httpMethod });

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(IEnumerable<string> httpMethods)
            => this.RestrictingForHttpMethods(httpMethods.Select(m => new HttpMethod(m)));

        /// <inheritdoc />
        public IAndActionAttributesTestBuilder RestrictingForHttpMethods(params string[] httpMethods)
            => this.RestrictingForHttpMethods(httpMethods.AsEnumerable());

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
            => this.RestrictingForHttpMethods(httpMethods.AsEnumerable());

        /// <inheritdoc />
        public IActionAttributesTestBuilder AndAlso() => this;

        protected override IAndActionAttributesTestBuilder GetAttributesTestBuilder() => this;

        protected override void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "{0} action to have {1}, but {2}.",
                this.TestContext.ExceptionMessagePrefix,
                expectedValue,
                actualValue));
        }
    }
}
