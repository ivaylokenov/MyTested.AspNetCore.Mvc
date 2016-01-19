namespace MyTested.Mvc.Utilities.Validators
{
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using System;
    using System.Collections.Generic;
    public static class RouteActionResultValidator
    {
        public static void ValidateActionName(
            dynamic actionResult,
            string actionName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualActionName = actionResult.ActionName;
                if (actionName != actualActionName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{actionName}' action name",
                        string.Format("instead received '{0}'", actualActionName != null ? actualActionName : "null"));
                }
            });
        }

        public static void ValidateControllerName(
            dynamic actionResult,
            string controllerName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualControllerName = actionResult.ControllerName;
                if (controllerName != actualControllerName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{controllerName}' controller name",
                        string.Format("instead received '{0}'", actualControllerName != null ? actualControllerName : "null"));
                }
            });
        }

        public static void ValidateRouteName(
            dynamic actionResult,
            string routeName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualRouteName = actionResult.RouteName;

                if (routeName != actualRouteName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{routeName}' route name",
                        string.Format("instead received '{0}'", actualRouteName != null ? actualRouteName : "null"));
                }
            });
        }

        public static void ValidateRouteValue(
            dynamic actionResult,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                if (!actionResult.RouteValues.ContainsKey(key))
                {
                    failedValidationAction(
                        "route values",
                        $"to have item with key '{key}'",
                        "such was not found");
                }
            });
        }

        public static void ValidateRouteValue(
            dynamic actionResult,
            string key,
            object value,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = actionResult.RouteValues;

                var itemExists = routeValues.ContainsKey(key);
                var actualValue = itemExists ? routeValues[key] : null;

                if (!itemExists || Reflection.AreNotDeeplyEqual(value, actualValue))
                {
                    failedValidationAction(
                        "route values",
                        $"to have item with key '{key}' and the provided value",
                        $"{(itemExists ? $"the value was different" : "such was not found")}");
                }
            });
        }

        public static void ValidateRouteValues(
            dynamic actionResult,
            IDictionary<string, object> routeValues,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualRouteValues = actionResult.RouteValues;

                var expectedItems = routeValues.Count;
                var actualItems = actualRouteValues.Count;

                if (expectedItems != actualItems)
                {
                    failedValidationAction(
                        "route values",
                        $"to have {expectedItems} custom {(expectedItems != 1 ? "items" : "item")}",
                        $"in fact found {actualItems}");
                }

                routeValues.ForEach(item => ValidateRouteValue(
                    actionResult,
                    item.Key,
                    item.Value,
                    failedValidationAction));
            });
        }

        public static void ValidateUrlHelper(
            dynamic actionResult,
            IUrlHelper urlHelper,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                if (urlHelper != actionResult.UrlHelper)
                {
                    failedValidationAction(
                        "UrlHelper",
                        "to be the same as the provided one",
                        "instead received different result");
                }
            });
        }

        public static void ValidateUrlHelperOfType<TUrlHelper>(
            dynamic actionResult,
            Action<string, string, string> failedValidationAction)
            where TUrlHelper : IUrlHelper
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualUrlHelper = actionResult.UrlHelper;

                if (actualUrlHelper == null ||
                    Reflection.AreDifferentTypes(typeof(TUrlHelper), actualUrlHelper.GetType()))
                {
                    failedValidationAction(
                        "UrlHelper",
                        $"to be of {typeof(TUrlHelper).Name} type",
                        $"instead received {actualUrlHelper.GetName()}");
                }
            });
        }
    }
}
