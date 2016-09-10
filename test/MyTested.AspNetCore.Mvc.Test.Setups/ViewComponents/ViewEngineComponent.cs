using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    public class ViewEngineComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewEngine viewEngine) => new ViewViewComponentResult
        {
            ViewEngine = viewEngine
        };
    }
}
