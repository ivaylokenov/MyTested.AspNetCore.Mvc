namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class MultipleDbContextController : Controller
    {
        private readonly CustomDbContext data;
        private readonly AnotherDbContext anotherData;

        public MultipleDbContextController(
            CustomDbContext data, 
            AnotherDbContext anotherData)
        {
            this.data = data;
            this.anotherData = anotherData;
        }

        public IActionResult Create(CustomModel model, AnotherModel anotherModel)
        {
            this.data.Models.Add(model);
            this.anotherData.OtherModels.Add(anotherModel);
            
            return this.Ok();
        }

        public IActionResult Find(int id)
        {
            var model = this.data.Models.Find(id);
            var anotherModel = this.anotherData.OtherModels.Find(id);

            if (model == null || anotherModel == null)
            {
                return this.NotFound();
            }

            return this.Ok(new
            {
                Model = model.Name,
                AnotherModel = anotherModel.FullName
            });
        }
    }
}
