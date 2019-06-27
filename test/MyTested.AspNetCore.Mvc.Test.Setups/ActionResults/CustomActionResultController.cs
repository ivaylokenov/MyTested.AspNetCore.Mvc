namespace MyTested.AspNetCore.Mvc.Test.Setups.ActionResults
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomActionResultController : Controller
    {
        public IActionResult CustomActionResult()
            => new CustomActionResult("Value")
            {
                CustomProperty = "CustomValue"
            };
    }
}
