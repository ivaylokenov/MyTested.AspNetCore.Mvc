namespace MyTested.Mvc.Internal.Routes
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Mvc;
    using Application;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Routing;
    using Utilities;

    public static class RouteExpressionParser
    {
        // This key should be ignored as it is used internally for route attribute matching.
        private static readonly string RouteGroupKey = "!__route_group";

        public static ExpressionParsedRouteContext Parse<TController>(
            LambdaExpression actionCallExpression,
            object additionalRouteValues = null)
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

            var routeValues = GetRouteValues(methodInfo, methodCallExpression, controllerActionDescriptor);

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

            AddControllerAndActionToRouteValues(controllerName, actionName, routeValues);

            return new ExpressionParsedRouteContext(
                controllerType,
                controllerName,
                actionName,
                routeValues);
        }

        private static IDictionary<string, object> GetRouteValues(
            MethodInfo methodInfo,
            MethodCallExpression methodCallExpression,
            ControllerActionDescriptor controllerActionDescriptor)
        {
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            var arguments = methodCallExpression.Arguments;
            if (arguments.Count == 0)
            {
                return result;
            }

            var methodParameterNames = methodInfo.GetParameters();

            var parameterDescriptors = new Dictionary<string, string>();
            var parameters = controllerActionDescriptor.Parameters;
            for (int i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                if (parameter.BindingInfo != null)
                {
                    parameterDescriptors.Add(parameter.Name, parameter.BindingInfo.BinderModelName);
                }
            }

            for (var i = 0; i < arguments.Count; i++)
            {
                var methodParameterName = methodParameterNames[i].Name;
                if (parameterDescriptors.ContainsKey(methodParameterName))
                {
                    methodParameterName = parameterDescriptors[methodParameterName];
                }

                var expressionArgument = arguments[i];

                if (expressionArgument.NodeType == ExpressionType.Call)
                {
                    // Expression of type c => c.Action(With.No<int>()) - value should be ignored and can be skipped.
                    var expressionArgumentAsMethodCall = (MethodCallExpression)expressionArgument;
                    if (expressionArgumentAsMethodCall.Object == null
                        && expressionArgumentAsMethodCall.Method.DeclaringType == typeof(With))
                    {
                        continue;
                    }
                }

                object value;
                if (expressionArgument.NodeType == ExpressionType.Constant)
                {
                    // Expression of type c => c.Action({const}) - value can be extracted without compiling.
                    value = ((ConstantExpression)expressionArgument).Value;
                }
                else
                {
                    // Expresion needs compiling because it is not of constant type.
                    var convertExpression = Expression.Convert(expressionArgument, typeof(object));
                    value = Expression.Lambda<Func<object>>(convertExpression).Compile().Invoke();
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

        private static void AddControllerAndActionToRouteValues(string controllerName, string actionName, IDictionary<string, object> routeValues)
        {
            routeValues["controller"] = controllerName;
            routeValues["action"] = actionName;
        }
    }
}
