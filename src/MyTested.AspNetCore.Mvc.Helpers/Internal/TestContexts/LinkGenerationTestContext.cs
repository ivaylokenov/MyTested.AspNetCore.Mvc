namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class LinkGenerationTestContext
    {
        public string Location { get; set; }

        public IUrlHelper UrlHelper { get; set; }

        public string RouteName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public IDictionary<string, object> RouteValues { get; set; }
        
        public static LinkGenerationTestContext FromCreatedResult(IActionResult actionResult)
        {
            if (actionResult is CreatedAtRouteResult createdAtRouteResult)
            {
                createdAtRouteResult.RouteValues = createdAtRouteResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = createdAtRouteResult.UrlHelper,
                    RouteName = createdAtRouteResult.RouteName,
                    RouteValues = new SortedDictionary<string, object>(createdAtRouteResult.RouteValues)
                };
            }

            if (actionResult is CreatedAtActionResult createdAtActionResult)
            {
                createdAtActionResult.RouteValues = createdAtActionResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = createdAtActionResult.UrlHelper,
                    Controller = createdAtActionResult.ControllerName,
                    Action = createdAtActionResult.ActionName,
                    RouteValues = new SortedDictionary<string, object>(createdAtActionResult.RouteValues)
                };
            }

            if (actionResult is CreatedResult createdResult)
            {
                return new LinkGenerationTestContext
                {
                    Location = createdResult.Location
                };
            }

            return null;
        }

        public static LinkGenerationTestContext FromRedirectResult(IActionResult actionResult)
        {
            if (actionResult is RedirectToRouteResult redirectToRouteResult)
            {
                redirectToRouteResult.RouteValues = redirectToRouteResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = redirectToRouteResult.UrlHelper,
                    RouteName = redirectToRouteResult.RouteName,
                    RouteValues = new SortedDictionary<string, object>(redirectToRouteResult.RouteValues)
                };
            }

            if (actionResult is RedirectToActionResult redirectToActionResult)
            {
                redirectToActionResult.RouteValues = redirectToActionResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = redirectToActionResult.UrlHelper,
                    Controller = redirectToActionResult.ControllerName,
                    Action = redirectToActionResult.ActionName,
                    RouteValues = new SortedDictionary<string, object>(redirectToActionResult.RouteValues)
                };
            }

            if (actionResult is RedirectResult redirectResult)
            {
                return new LinkGenerationTestContext
                {
                    Location = redirectResult.Url,
                    UrlHelper = redirectResult.UrlHelper
                };
            }

            return null;
        }

        public static LinkGenerationTestContext FromLocalRedirectResult(LocalRedirectResult localRedirectResult)
        {
            return new LinkGenerationTestContext
            {
                Location = localRedirectResult.Url,
                UrlHelper = localRedirectResult.UrlHelper
            };
        }
    }
}
