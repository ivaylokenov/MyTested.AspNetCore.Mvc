namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class OptionsController : Controller
    {
        private readonly CustomSettings settings;

        public OptionsController(IOptions<CustomSettings> settings)
        {
            this.settings = settings.Value;
        }

        public IActionResult Index()
        {
            if (this.settings.Name == "Test")
            {
                return this.Ok();
            }

            return this.BadRequest();
        }
    }
}
