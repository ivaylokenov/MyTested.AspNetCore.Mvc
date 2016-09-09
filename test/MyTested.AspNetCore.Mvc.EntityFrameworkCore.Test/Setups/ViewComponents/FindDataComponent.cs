namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class FindDataComponent : ViewComponent
    {
        private readonly CustomDbContext data;

        public FindDataComponent(CustomDbContext data)
        {
            this.data = data;
        }

        public IViewComponentResult Invoke(int id)
        {
            var model = this.data.Models.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return this.Content("Invalid");
            }

            return this.View(model);
        }
    }
}
