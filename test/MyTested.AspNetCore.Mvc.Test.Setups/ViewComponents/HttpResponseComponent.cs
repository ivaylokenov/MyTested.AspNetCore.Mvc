namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class HttpResponseComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            TestObjectFactory.SetCustomHttpResponseProperties(this.HttpContext.Response);
            return this.View();
        }
    }
}
