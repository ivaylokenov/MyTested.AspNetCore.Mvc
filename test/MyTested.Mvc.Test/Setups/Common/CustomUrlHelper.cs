namespace MyTested.Mvc.Tests.Setups.Common
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Routing;

    public class CustomUrlHelper : IUrlHelper
    {
        public string Action(UrlActionContext actionContext)
        {
            return null;
        }

        public string Content(string contentPath)
        {
            return null;
        }

        public bool IsLocalUrl(string url)
        {
            return false;
        }

        public string Link(string routeName, object values)
        {
            return null;
        }

        public string RouteUrl(UrlRouteContext routeContext)
        {
            return null;
        }
    }
}
