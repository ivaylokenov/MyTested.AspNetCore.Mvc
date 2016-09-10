namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class CreateDataComponent : ViewComponent
    {
        private readonly CustomDbContext data;

        public CreateDataComponent(CustomDbContext data)
        {
            this.data = data;
        }

        public IViewComponentResult Invoke(CustomModel model)
        {
            this.data.Models.Add(model);
            return this.View();
        }
    }
}
