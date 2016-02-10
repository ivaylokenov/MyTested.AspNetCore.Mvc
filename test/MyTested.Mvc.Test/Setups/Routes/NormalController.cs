namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    public class NormalController : Controller
    {
        public static void StaticCall()
        {
        }

        public IActionResult ActionWithoutParameters()
        {
            return null;
        }

        public IActionResult ActionWithParameters(int id)
        {
            return null;
        }

        public IActionResult ActionWithStringParameters(string id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult ActionWithModel(int id, [FromBody]RequestModel model)
        {
            return null;
        }

        public IActionResult ActionWithMultipleParameters(int id, string text, [FromBody]RequestModel model)
        {
            return null;
        }

        public IActionResult ActionWithOverloads()
        {
            return null;
        }

        public IActionResult ActionWithOverloads(int? id)
        {
            return null;
        }

        [ActionName("AnotherName")]
        public IActionResult ActionWithChangedName()
        {
            return null;
        }

        public IActionResult QueryString(string first, int second)
        {
            return null;
        }

        [HttpGet]
        public IActionResult GetMethod()
        {
            return null;
        }

        public void VoidAction()
        {
        }
    }
}
