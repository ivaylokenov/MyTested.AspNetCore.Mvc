namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;

    public class CustomUrlHelper : IUrlHelper
    {
        public ActionContext ActionContext
        {
            get
            {
                return null;
            }
        }

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
