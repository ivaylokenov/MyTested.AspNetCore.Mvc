namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    [Custom]
    [ViewComponent(Name = "Test")]
    public class AttributesComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => this.View();
    }
}
