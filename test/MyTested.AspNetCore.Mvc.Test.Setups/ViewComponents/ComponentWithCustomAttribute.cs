namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    [Custom]
    public class ComponentWithCustomAttribute : ViewComponent
    {
        public IViewComponentResult Invoke() => this.View();
    }
}
