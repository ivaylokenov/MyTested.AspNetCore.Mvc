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
            return this.Ok();
        }

        public IActionResult Find(int id)
        {
            var model = this.data.Models.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }
    }
}
