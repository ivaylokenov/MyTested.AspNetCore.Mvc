namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ResponseCache]
    public class InheritAttributesController : InheritAttributesBaseController
    {
    }

    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class InheritAttributesBaseController : ControllerBase
    {

    }
}
