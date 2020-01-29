namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Utilities.Extensions;

    public class ModelBindingActionInvokerCache
    {
        private dynamic instance;

        public dynamic GetCachedResult(ControllerContext controllerContext)
        {
            if (this.instance == null)
            {
                var type = WebFramework.Internals.ControllerActionInvokerCache;
                this.instance = TestServiceProvider.GetService(type).Exposed();
            }
            
            return this.instance.GetCachedResult(controllerContext);
        }
    }
}
