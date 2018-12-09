namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Internal.Routing;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    public static class UrlHelperExtensions
    {
        public static string ExpressionLink(
            this IUrlHelper urlHelper,
            LambdaExpression lambdaExpression,
            out ICollection<string> ignoredRouteKeys)
        {
            var routeValues = RouteExpressionParser.Parse(lambdaExpression, considerParameterDescriptors: true);

            var ignoredKeys = routeValues
                .ActionArguments
                .Where(a => ExpressionParser.IsIgnoredArgument(a.Value.Value))
                .Select(a => a.Key)
                .ToList();
            
            ignoredRouteKeys = ignoredKeys;

            return urlHelper.Action(
                routeValues.Action,
                routeValues.ControllerName,
                routeValues.ActionArguments.ToSortedRouteValues(kvp => !ignoredKeys.Contains(kvp.Key)));
        }

        public static string GenerateLink(
            this IUrlHelper urlHelper,
            LinkGenerationTestContext linkGenerationTestContext,
            ControllerTestContext controllerTestContext,
            ICollection<string> ignoredRouteKeys = null)
        {
            if (ignoredRouteKeys != null)
            {
                linkGenerationTestContext.RouteValues = linkGenerationTestContext
                    .RouteValues
                    ?.Where(kvp => !ignoredRouteKeys.Contains(kvp.Key))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            string uri = null;
            if (!string.IsNullOrWhiteSpace(linkGenerationTestContext.Location))
            {
                uri = linkGenerationTestContext.Location;
            }
            else if (!string.IsNullOrWhiteSpace(linkGenerationTestContext.RouteName))
            {
                uri = urlHelper.RouteUrl(
                    linkGenerationTestContext.RouteName,
                    linkGenerationTestContext.RouteValues);
            }
            else
            {
                linkGenerationTestContext.Action = linkGenerationTestContext.Action
                    ?? controllerTestContext.RouteData.Values["action"] as string
                    ?? controllerTestContext.MethodName;

                linkGenerationTestContext.Controller = linkGenerationTestContext.Controller
                    ?? controllerTestContext.RouteData.Values["controller"] as string
                    ?? controllerTestContext.ComponentContext.ActionDescriptor.ControllerName;

                uri = urlHelper.Action(
                    linkGenerationTestContext.Action,
                    linkGenerationTestContext.Controller,
                    linkGenerationTestContext.RouteValues);
            }

            return uri;
        }
    }
}
