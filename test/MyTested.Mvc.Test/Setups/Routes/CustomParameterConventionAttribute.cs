namespace MyTested.Mvc.Tests.Setups.Routes
{
    using System;
    using Microsoft.AspNet.Mvc.ApplicationModels;
    using Microsoft.AspNet.Mvc.ModelBinding;

    public class CustomParameterConventionAttribute : Attribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
            parameter.BindingInfo.BinderModelName = "ChangedParameter";
        }
    }
}
