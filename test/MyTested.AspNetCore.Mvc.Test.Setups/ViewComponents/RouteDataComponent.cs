namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class RouteDataComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (this.RouteData.Values.ContainsKey("Explicit"))
            {
                return this.Content(this.RouteData.Values["Explicit"] as string);
            }

            return this.Content(this.Url.Action());
        }
    }
}
