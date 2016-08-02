namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System.Linq.Expressions;
    using Internal.Routing;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    public static class UrlHelperExtensions
    {
        public static string ExpressionLink(this IUrlHelper urlHelper, LambdaExpression lambdaExpression)
        {
            var routeValues = RouteExpressionParser.Parse(lambdaExpression, considerParameterDescriptors: true);
            return urlHelper.Action(
                routeValues.Action,
                routeValues.ControllerName,
                routeValues.ActionArguments.ToSortedRouteValues());
        }

        public static string GenerateLink(
            this IUrlHelper urlHelper,
            LinkGenerationTestContext linkGenerationTestContext,
            ControllerTestContext controllerTestContext)
        {
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
                    ?? controllerTestContext.ControllerContext.ActionDescriptor.ControllerName;

                uri = urlHelper.Action(
                    linkGenerationTestContext.Action,
                    linkGenerationTestContext.Controller,
                    linkGenerationTestContext.RouteValues);
            }

            return uri;
        }
    }
}
