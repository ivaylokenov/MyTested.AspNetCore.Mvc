namespace MyTested.Mvc.Internal.Controllers
{
    using Contracts;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System;
    using System.Collections.Generic;

    internal class ValidControllersCache : IValidControllersCache, IControllerModelConvention
    {
        // hash set will be synchronized because after initialization only 'Contains' method will be invoked
        private static HashSet<Type> validControllersCache = new HashSet<Type>();
        
        public void Apply(ControllerModel controller)
        {
            validControllersCache.Add(controller.ControllerType.AsType());
        }

        public bool IsValid(Type controllerType)
        {
            return validControllersCache.Contains(controllerType);
        }
    }
}
