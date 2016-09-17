namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ArgumentsComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id, RequestModel model) => this.Content($"{id},{model?.RequiredString}");
    }
}
