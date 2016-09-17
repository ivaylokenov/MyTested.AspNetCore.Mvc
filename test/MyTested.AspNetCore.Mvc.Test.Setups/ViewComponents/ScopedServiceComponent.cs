namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class ScopedServiceComponent: ViewComponent
    {
        private readonly IScopedService scopedService;

        public ScopedServiceComponent(IScopedService scopedService)
        {
            this.scopedService = scopedService;
        }

        public IViewComponentResult Invoke()
        {
            return this.View(this.scopedService.Value);
        }
    }
}
