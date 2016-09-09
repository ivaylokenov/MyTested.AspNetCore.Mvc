namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class OptionsComponent : ViewComponent
    {
        private readonly CustomSettings settings;

        public OptionsComponent(IOptions<CustomSettings> settings)
        {
            this.settings = settings.Value;
        }

        public IViewComponentResult Invoke()
        {
            if (this.settings.Name == "Test")
            {
                return this.View();
            }

            return this.Content("Invalid");
        }
    }
}
