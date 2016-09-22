namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    
    public class NormalComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => this.Content("Test");
    }
}
