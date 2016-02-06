namespace MyTested.Mvc.Tests.Setups.Routes
{
    using System;
    using Microsoft.AspNet.Mvc.ApplicationModels;

    public class CustomActionConventionAttribute : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            action.ActionName = "ChangedAction";
        }
    }
}
