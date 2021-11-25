namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using System.Linq;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class DbContextController : Controller
    {
        private readonly CustomDbContext data;

        public DbContextController(CustomDbContext data)
        {
            this.data = data;
        }

        public IActionResult Create(CustomModel model)
        {
            this.data.Models.Add(model);
            this.data.SaveChanges();

            return this.Ok();
        }

        public IActionResult Get(int id)
        {
            var model = this.data.Models.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        public IActionResult GetAll()
        {
            var models = this.data.Models;
            if (models == null || !models.Any())
            {
                return this.NotFound();
            }

            return this.Ok(models.ToList());
        }

        public IActionResult Update(int id)
        {
            var model = new CustomModel
            {
                Id = id,
                Name = "Updated"
            };

            this.data.Update(model);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
