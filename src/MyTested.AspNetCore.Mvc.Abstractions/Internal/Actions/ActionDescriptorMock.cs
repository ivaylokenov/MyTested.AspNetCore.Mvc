namespace MyTested.AspNetCore.Mvc.Internal.Actions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ActionDescriptorMock : ActionDescriptor
    {
        public static ActionDescriptor Default
            => new ControllerActionDescriptor
            {
                ActionConstraints = new List<IActionConstraintMetadata>(),
                AttributeRouteInfo = new AttributeRouteInfo(),
                BoundProperties = new List<ParameterDescriptor>(),
                FilterDescriptors = new List<FilterDescriptor>(),
                Parameters = new List<ParameterDescriptor>(),
                Properties = new Dictionary<object, object>(),
                RouteValues = new Dictionary<string, string>(),
            };
    }
}
