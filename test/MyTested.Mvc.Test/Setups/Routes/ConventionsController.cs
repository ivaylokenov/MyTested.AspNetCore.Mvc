namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    [CustomControllerConvention]
    public class ConventionsController
    {
        [CustomActionConvention]
        public IActionResult ConventionsAction([CustomParameterConvention]int id)
        {
            return null;
        }
    }
}
