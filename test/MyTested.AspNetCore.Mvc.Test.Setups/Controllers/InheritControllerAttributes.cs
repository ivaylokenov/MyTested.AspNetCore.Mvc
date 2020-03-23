namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ResponseCache]
    public class InheritControllerAttributes : InheritAttributesBaseController
    {
        public override uint MethodA()
            => 10;
    }

    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public abstract class InheritAttributesBaseController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public abstract uint MethodA();
    }
}
