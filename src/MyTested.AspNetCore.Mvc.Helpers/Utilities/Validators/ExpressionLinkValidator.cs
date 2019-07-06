namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Extensions;
    using Internal.TestContexts;
    using Internal.Application;
    using Internal.Services;
    using Microsoft.AspNetCore.Mvc.Routing;

    /// <summary>
    /// Validator class containing validation logic for expression links.
    /// </summary>
    public static class ExpressionLinkValidator
    {
        public static void Validate(
            ControllerTestContext controllerTestContext,
            LinkGenerationTestContext linkGenerationTestContext,
            LambdaExpression expectedRouteValuesAsLambdaExpression,
            Action<string, string, string> failedValidationAction)
        {
            var actionContext = controllerTestContext.ComponentContext;
            if (!actionContext.RouteData.Routers.Any())
            {
                actionContext.RouteData.Routers.Add(TestApplication.Router);
            }

            var urlHelper = linkGenerationTestContext.UrlHelper ?? TestServiceProvider
                .GetRequiredService<IUrlHelperFactory>()
                .GetUrlHelper(actionContext);

            var expectedUri = urlHelper.ExpressionLink(
                expectedRouteValuesAsLambdaExpression,
                out var ignoredRouteKeys);

            var actualUri = urlHelper.GenerateLink(
                linkGenerationTestContext,
                controllerTestContext,
                ignoredRouteKeys);

            if (!string.Equals(expectedUri, actualUri, StringComparison.OrdinalIgnoreCase))
            {
                failedValidationAction(
                    "to have resolved location to",
                    $"{expectedUri.GetErrorMessageName()}",
                    $"in fact received {actualUri.GetErrorMessageName()}");
            }
        }
    }
}
