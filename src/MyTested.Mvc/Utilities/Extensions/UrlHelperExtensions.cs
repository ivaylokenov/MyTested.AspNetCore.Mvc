namespace MyTested.Mvc.Utilities.Extensions
{
    using System.Linq.Expressions;
    using Internal.Routes;
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
                    ?? controllerTestContext.RouteData.Values["action"] as string;

                linkGenerationTestContext.Controller = linkGenerationTestContext.Controller
                    ?? controllerTestContext.RouteData.Values["controller"] as string;

                uri = urlHelper.Action(
                    linkGenerationTestContext.Action,
                    linkGenerationTestContext.Controller,
                    linkGenerationTestContext.RouteValues);
            }

            return uri;
        }
    }
}
