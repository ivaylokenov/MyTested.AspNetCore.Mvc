namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    internal class ValidControllersCache : IValidControllersCache, IControllerModelConvention
    {
        // hash set will be synchronized because after initialization only 'Contains' method will be invoked
        private static readonly HashSet<Type> ControllersCache = new HashSet<Type>();
        
        public void Apply(ControllerModel controller)
        {
            ControllersCache.Add(controller.ControllerType.AsType());
        }

        public bool IsValid(Type controllerType)
        {
            return ControllersCache.Contains(controllerType);
        }
    }
}
