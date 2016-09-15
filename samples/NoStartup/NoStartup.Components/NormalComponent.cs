namespace NoStartup.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class NormalComponent : ViewComponent
    {
        private readonly IService service;

        public NormalComponent(IService myService)
        {
            this.service = myService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => await Task.FromResult(this.View(this.service.GetData()));
    }
}
