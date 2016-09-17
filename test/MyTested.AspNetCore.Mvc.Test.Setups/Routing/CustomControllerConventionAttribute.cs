namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using System;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    public class CustomControllerConventionAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ControllerName = "ChangedController";
        }
    }
}
