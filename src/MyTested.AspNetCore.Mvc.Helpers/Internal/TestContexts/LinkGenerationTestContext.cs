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
            var createdAtRouteResult = actionResult as CreatedAtRouteResult;
            if (createdAtRouteResult != null)
            {
                createdAtRouteResult.RouteValues = createdAtRouteResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = createdAtRouteResult.UrlHelper,
                    RouteName = createdAtRouteResult.RouteName,
                    RouteValues = new SortedDictionary<string, object>(createdAtRouteResult.RouteValues)
                };
            }

            var createdAtActionResult = actionResult as CreatedAtActionResult;
            if (createdAtActionResult != null)
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

            var createdResult = actionResult as CreatedResult;
            if (createdResult != null)
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
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            if (redirectToRouteResult != null)
            {
                redirectToRouteResult.RouteValues = redirectToRouteResult.RouteValues ?? new RouteValueDictionary();

                return new LinkGenerationTestContext
                {
                    UrlHelper = redirectToRouteResult.UrlHelper,
                    RouteName = redirectToRouteResult.RouteName,
                    RouteValues = new SortedDictionary<string, object>(redirectToRouteResult.RouteValues)
                };
            }

            var redirectToActionResult = actionResult as RedirectToActionResult;
            if (redirectToActionResult != null)
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

            var redirectResult = actionResult as RedirectResult;
            if (redirectResult != null)
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
