namespace MyTested.Mvc.Internal.Routes
{
    using System;
    using System.Linq.Expressions;
    using Application;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Extensions;
    public static class RouteExpressionParser
    {
        // This key should be ignored as it is used internally for route attribute matching.
        private static readonly string RouteGroupKey = "!__route_group";

        public static ExpressionParsedRouteContext Parse(
            LambdaExpression actionCallExpression,
            object additionalRouteValues = null,
            bool considerParameterDescriptors = false)
        {
            var methodCallExpression = ExpressionParser.GetMethodCallExpression(actionCallExpression);

            var controllerType = methodCallExpression.Object.Type;
            
            var methodInfo = methodCallExpression.Method;

            var controllerActionDescriptorCache = TestServiceProvider.GetRequiredService<IControllerActionDescriptorCache>();
            var controllerActionDescriptor = controllerActionDescriptorCache.GetActionDescriptor(methodInfo);
            
            if (controllerActionDescriptor == null)
            {
                throw new InvalidOperationException($"Method {methodInfo.Name} in class {methodInfo.DeclaringType.Name} is not a valid controller action.");
            }

            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.Name;

            var routeValues = GetRouteValues(methodInfo, methodCallExpression, controllerActionDescriptor, considerParameterDescriptors);

            // If there is a route constraint with specific expected value, add it to the result.
            var routeConstraints = controllerActionDescriptor.RouteConstraints;
            for (int i = 0; i < routeConstraints.Count; i++)
            {
                var routeConstraint = routeConstraints[i];
                var routeKey = routeConstraint.RouteKey;
                var routeValue = routeConstraint.RouteValue;

                if (string.Equals(routeKey, RouteGroupKey))
                {
                    continue;
                }

                if (routeValue != string.Empty)
                {
                    // Override the 'default' values.
                    if (string.Equals(routeKey, "controller", StringComparison.OrdinalIgnoreCase))
                    {
                        controllerName = routeValue;
                    }
                    else if (string.Equals(routeKey, "action", StringComparison.OrdinalIgnoreCase))
                    {
                        actionName = routeValue;
                    }
                    else
                    {
                        routeValues[routeConstraint.RouteKey] = routeValue;
                    }
                }
            }

            ApplyAdditionaRouteValues(additionalRouteValues, routeValues);
            
            return new ExpressionParsedRouteContext(
                controllerType,
                controllerName,
                actionName,
                routeValues);
        }

        public static RouteData ResolveRouteData(IRouter router, LambdaExpression actionCallExpression)
        {
            var parsedRouteContext = Parse(actionCallExpression, considerParameterDescriptors: true);
            
            var routeData = new RouteData();

            parsedRouteContext.ActionArguments.ForEach(r => routeData.Values.Add(r.Key, r.Value.Value));
            routeData.Values["controller"] = parsedRouteContext.ControllerName;
            routeData.Values["action"] = parsedRouteContext.Action;

            routeData.Routers.Add(router);

            return routeData;
        }

        private static IDictionary<string, object> GetRouteValues(
            MethodInfo methodInfo,
            MethodCallExpression methodCallExpression,
            ControllerActionDescriptor controllerActionDescriptor,
            bool considerParameterDescriptors)
        {
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            var arguments = methodCallExpression.Arguments;
            if (arguments.Count == 0)
            {
                return result;
            }

            var methodParameters = methodInfo.GetParameters();

            var parameterDescriptors = new Dictionary<string, string>();
            if (considerParameterDescriptors)
            {
                var parameters = controllerActionDescriptor.Parameters;
                for (int i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters[i];
                    if (parameter.BindingInfo != null)
                    {
                        parameterDescriptors.Add(parameter.Name, parameter.BindingInfo.BinderModelName);
                    }
                }
            }

            for (var i = 0; i < arguments.Count; i++)
            {
                var methodParameterName = methodParameters[i].Name;
                if (considerParameterDescriptors && parameterDescriptors.ContainsKey(methodParameterName))
                {
                    methodParameterName = parameterDescriptors[methodParameterName] ?? methodParameterName;
                }

                var value = ExpressionParser.ResolveExpressionValue(arguments[i]);
                if (value == null)
                {
                    continue;
                }

                result[methodParameterName] = value;
            }

            return result;
        }

        private static void ApplyAdditionaRouteValues(object routeValues, IDictionary<string, object> result)
        {
            if (routeValues != null)
            {
                var additionalRouteValues = new RouteValueDictionary(routeValues);

                foreach (var additionalRouteValue in additionalRouteValues)
                {
                    result[additionalRouteValue.Key] = additionalRouteValue.Value;
                }
            }
        }
    }
}
