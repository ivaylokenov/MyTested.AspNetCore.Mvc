namespace MyTested.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class LinkGenerationTestContext
    {
        public static LinkGenerationTestContext FromCreatedResult(IActionResult actionResult)
        {
            var createdAtRouteResult = actionResult as CreatedAtRouteResult;
            if (createdAtRouteResult != null)
            {
                return new LinkGenerationTestContext
                {
                    UrlHelper = createdAtRouteResult.UrlHelper,
                    RouteName = createdAtRouteResult.RouteName,
                    RouteValues = createdAtRouteResult.RouteValues
                };
            }

            var createdAtActionResult = actionResult as CreatedAtActionResult;
            if (createdAtActionResult != null)
            {
                return new LinkGenerationTestContext
                {
                    UrlHelper = createdAtActionResult.UrlHelper,
                    Controller = createdAtActionResult.ControllerName,
                    Action = createdAtActionResult.ActionName,
                    RouteValues = createdAtActionResult.RouteValues,
                };
            }

            var createdResult = actionResult as CreatedResult;
            if (createdResult != null)
            {
                return new LinkGenerationTestContext { Location = createdResult.Location };
            }

            return null;
        }

        public static LinkGenerationTestContext FromRedirectResult(IActionResult actionResult)
        {
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            if (redirectToRouteResult != null)
            {
                return new LinkGenerationTestContext
                {
                    UrlHelper = redirectToRouteResult.UrlHelper,
                    RouteName = redirectToRouteResult.RouteName,
                    RouteValues = redirectToRouteResult.RouteValues
                };
            }

            var redirectToActionResult = actionResult as RedirectToActionResult;
            if (redirectToActionResult != null)
            {
                return new LinkGenerationTestContext
                {
                    UrlHelper = redirectToActionResult.UrlHelper,
                    Controller = redirectToActionResult.ControllerName,
                    Action = redirectToActionResult.ActionName,
                    RouteValues = redirectToActionResult.RouteValues,
                };
            }

            var redirectResult = actionResult as RedirectResult;
            if (redirectResult != null)
            {
                return new LinkGenerationTestContext { Location = redirectResult.Url };
            }

            return null;
        }

        public string Location { get; set; }

        public IUrlHelper UrlHelper { get; set; }

        public string RouteName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public IDictionary<string, object> RouteValues { get; set; }
    }
}
