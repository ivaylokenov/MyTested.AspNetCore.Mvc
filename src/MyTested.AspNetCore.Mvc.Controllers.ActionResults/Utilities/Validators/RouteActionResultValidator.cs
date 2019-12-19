﻿namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Validator class containing validation logic for action results with route specific information.
    /// </summary>
    public static class RouteActionResultValidator
    {
        private const string RouteValuesName = "route values";

        /// <summary>
        /// Validates whether ActionName is the same as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with ActionName.</param>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateActionName(
            dynamic actionResult,
            string actionName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualActionName = (string)actionResult.ActionName;
                if (actionName != actualActionName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{actionName}' action name",
                        $"instead received {actualActionName.GetErrorMessageName()}");
                }
            });
        }

        /// <summary>
        /// Validates whether ControllerName is the same as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with ControllerName.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateControllerName(
            dynamic actionResult,
            string controllerName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualControllerName = (string)actionResult.ControllerName;
                if (controllerName != actualControllerName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{controllerName}' controller name",
                        $"instead received {actualControllerName.GetErrorMessageName()}");
                }
            });
        }

        /// <summary>
        /// Validates whether RouteName is the same as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with RouteName.</param>
        /// <param name="routeName">Expected route name.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateRouteName(
            dynamic actionResult,
            string routeName,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualRouteName = (string)actionResult.RouteName;

                if (routeName != actualRouteName)
                {
                    failedValidationAction(
                        "to have",
                        $"'{routeName}' route name",
                        $"instead received {actualRouteName.GetErrorMessageName()}");
                }
            });
        }

        /// <summary>
        /// Validates whether RouteValues contains route entry with key as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with RouteValues.</param>
        /// <param name="key">Expected route entry key.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateRouteValue(
            dynamic actionResult,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateStringKey(
                    RouteValuesName,
                    routeValues,
                    key,
                    failedValidationAction);
            });
        }

        /// <summary>
        /// Validates whether RouteValues contains route entries with key and value as the provided ones from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with RouteValues.</param>
        /// <param name="key">Expected route entry key.</param>
        /// <param name="value">Expected route entry value.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateRouteValue(
            dynamic actionResult,
            string key,
            object value,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateStringKeyAndValue(
                    RouteValuesName,
                    routeValues,
                    key,
                    value,
                    failedValidationAction);
            });
        }

        public static void ValidateRouteValueOfType<TRouteValue>(
            dynamic actionResult,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateValueOfType<TRouteValue>(
                    RouteValuesName,
                    routeValues,
                    failedValidationAction);
            });
        }

        public static void ValidateRouteValueOfType<TRouteValue>(
            dynamic actionResult,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateStringKeyAndValueOfType<TRouteValue>(
                    RouteValuesName,
                    routeValues,
                    key,
                    failedValidationAction);
            });
        }

        public static void ValidateRouteValue<TRouteValue>(
            dynamic actionResult,
            TRouteValue value,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var routeValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateValue(
                    RouteValuesName,
                    routeValues,
                    value,
                    failedValidationAction);
            });
        }

        /// <summary>
        /// Validates whether RouteValues contains the same route entries as the provided ones from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with RouteValues.</param>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <param name="includeCountCheck">Specifies whether the total number of route values should be validated.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateRouteValues(
            dynamic actionResult,
            IDictionary<string, object> routeValues,
            bool includeCountCheck,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualRouteValues = (IDictionary<string, object>)actionResult.RouteValues;

                DictionaryValidator.ValidateValues(
                    RouteValuesName,
                    actualRouteValues,
                    routeValues,
                    failedValidationAction,
                    includeCountCheck);
            });
        }

        /// <summary>
        /// Validates whether UrlHelper contains the same value as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with UrlHelper.</param>
        /// <param name="urlHelper">Expected URL helper.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateUrlHelper(
            dynamic actionResult,
            IUrlHelper urlHelper,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                if (urlHelper != (IUrlHelper)actionResult.UrlHelper)
                {
                    failedValidationAction(
                        "URL helper",
                        "to be the same as the provided one",
                        "instead received different result");
                }
            });
        }

        /// <summary>
        /// Validates whether UrlHelper contains the same type as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with UrlHelper.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="urlHelperType"></param>
        public static void ValidateUrlHelperOfType(
            dynamic actionResult,
            Action<string, string, string> failedValidationAction,
            Type urlHelperType)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualUrlHelper = (IUrlHelper)actionResult.UrlHelper;
                if (actualUrlHelper == null ||
                    Reflection.AreDifferentTypes(urlHelperType, actualUrlHelper.GetType()))
                {
                    failedValidationAction(
                        "URL helper",
                        $"to be of {urlHelperType.Name} type",
                        $"instead received {actualUrlHelper.GetName()}");
                }
            });
        }

        /// <summary>
        /// Validates whether UrlHelper contains the same type as the provided one from action result containing such property.
        /// </summary>
        /// <typeparam name="TUrlHelper">Type of IUrlHelper.</typeparam>
        /// <param name="actionResult">Action result with UrlHelper.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateUrlHelperOfType<TUrlHelper>(
            dynamic actionResult,
            Action<string, string, string> failedValidationAction)
            where TUrlHelper : IUrlHelper
            => RouteActionResultValidator.ValidateUrlHelperOfType(actionResult, failedValidationAction, typeof(TUrlHelper));
    }
}
