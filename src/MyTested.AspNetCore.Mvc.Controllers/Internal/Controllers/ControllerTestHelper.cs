namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Services;

    public class ControllerTestHelper
    {
        public static void SetActionContextToAccessor(ActionContext actionContext)
        {
            var actionContextAccessor = TestServiceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor != null)
            {
                actionContextAccessor.ActionContext = actionContext;
            }
        }
    }
}
