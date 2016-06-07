namespace MyTested.AspNetCore.Mvc.Test.Setups.Routes
{
    using System;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    public class CustomActionConventionAttribute : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            action.ActionName = "ChangedAction";
        }
    }
}
