namespace MyTested.AspNetCore.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    [Area("MyArea")]
    public class InAreaController
    {
        public IActionResult Action(int id)
        {
            return null;
        }
    }
}
