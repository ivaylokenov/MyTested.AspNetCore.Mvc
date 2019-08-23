namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class ModelBindingActionInvokerCache
    {
        private dynamic methodDelegate;

        public dynamic GetCachedResult(ControllerContext controllerContext)
        {
            if (this.methodDelegate == null)
            {
                var type = WebFramework.Internals.ControllerActionInvokerCache;
                var instance = TestServiceProvider.GetService(type);
                var method = type.GetMethod(nameof(GetCachedResult));
                var typeOfDelegate = typeof(Func<,>).MakeGenericType(typeof(ControllerContext), method.ReturnType);
                this.methodDelegate = method.CreateDelegate(typeOfDelegate, instance);
            }
            
            return this.methodDelegate(controllerContext);
        }
    }
}
