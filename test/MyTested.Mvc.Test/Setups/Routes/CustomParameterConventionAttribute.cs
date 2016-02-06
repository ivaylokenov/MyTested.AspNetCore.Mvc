namespace MyTested.Mvc.Tests.Setups.Routes
{
    using System;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CustomParameterConventionAttribute : Attribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
            parameter.BindingInfo.BinderModelName = "ChangedParameter";
        }
    }
}
