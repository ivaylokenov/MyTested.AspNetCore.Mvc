namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class AsyncComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() => await Task.Run(() => this.View());
    }
}
