namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNet.Mvc;

    [MyRouteConstraint("controller", "CustomController")]
    public class RouteConstraintController
    {
        [MyRouteConstraint("action", "CustomAction")]
        [MyRouteConstraint("id", "5")]
        [MyRouteConstraint("key", "value")]
        public IActionResult Action(int id, int anotherId)
        {
            return null;
        }
    }
}
