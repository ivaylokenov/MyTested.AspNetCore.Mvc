namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
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
