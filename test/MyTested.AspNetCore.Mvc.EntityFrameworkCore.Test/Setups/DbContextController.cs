namespace MyTested.AspNetCore.Mvc.EntityFrameworkCore.Test.Setups
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
