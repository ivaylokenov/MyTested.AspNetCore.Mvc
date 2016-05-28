namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Linq.Expressions;
    using Authentication;
    using Contracts.Authentication;
    using Contracts.Controllers;
    using Contracts.Http;
    using Http;
    using Internal.Application;
    using Internal.Routes;
    using Microsoft.AspNetCore.Http;
    using Utilities.Validators;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext)
        {
            CommonValidator.CheckForNullReference(httpContext, nameof(HttpContext));
            this.TestContext.HttpContext = httpContext;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithHttpContext(Action<HttpContext> httpContextSetup)
        {
            httpContextSetup(this.HttpContext);
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithHttpRequest(HttpRequest httpRequest)
        {
            CommonValidator.CheckForNullReference(httpRequest, nameof(HttpRequest));
            this.HttpContext.CustomRequest = httpRequest;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithHttpRequest(Action<IHttpRequestBuilder> httpRequestBuilder)
        {
            var newHttpRequestBuilder = new HttpRequestBuilder();
            httpRequestBuilder(newHttpRequestBuilder);
            newHttpRequestBuilder.ApplyTo(this.HttpRequest);
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithAuthenticatedUser()
        {
            this.HttpContext.User = ClaimsPrincipalBuilder.DefaultAuthenticated;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IClaimsPrincipalBuilder> userBuilder)
        {
            var newUserBuilder = new ClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            this.HttpContext.User = newUserBuilder.GetClaimsPrincipal();
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithRouteData()
        {
            return this.WithRouteData(null);
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithRouteData(object additionalRouteValues)
        {
            this.resolveRouteValues = true;
            this.additionalRouteValues = additionalRouteValues;
            return this;
        }
        
        private void SetRouteData(LambdaExpression actionCall)
        {
            if (this.resolveRouteValues)
            {
                this.TestContext.RouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, actionCall);
                RouteExpressionParser.ApplyAdditionaRouteValues(
                    this.additionalRouteValues,
                    this.TestContext.RouteData.Values);
            }
        }
    }
}
