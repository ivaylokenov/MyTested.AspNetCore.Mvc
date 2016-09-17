namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Routing;
    using Utilities;

    /// <summary>
    /// Provides extension methods to all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        public static T TryCastTo<T>(this object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns the provided object casted as dynamic type.
        /// </summary>
        /// <returns>Object of dynamic type.</returns>
        public static dynamic AsDynamic(this object obj) => obj?.GetType().CastTo<dynamic>(obj);

        /// <summary>
        /// Gets friendly type name of object. Useful for generic types.
        /// </summary>
        /// <param name="obj">Object to get friendly name from.</param>
        /// <returns>Friendly name as string.</returns>
        public static string GetName(this object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            return obj.GetType().ToFriendlyTypeName();
        }

        /// <summary>
        /// Calls ToString on the provided object and returns the value. If the object is null, the provided optional name is returned.
        /// </summary>
        /// <param name="obj">Object to get error message name.</param>
        /// <param name="includeQuotes">Whether to include quotes around the error message name.</param>
        /// <param name="nullCaseName">Name to return in case of null object.</param>
        /// <returns>Error message name.</returns>
        public static string GetErrorMessageName(this object obj, bool includeQuotes = true, string nullCaseName = "null")
        {
            if (obj == null)
            {
                return nullCaseName;
            }

            var errorMessageName = obj.ToString();

            if (!includeQuotes)
            {
                return errorMessageName;
            }

            return $"'{errorMessageName}'";
        }

        public static IDictionary<string, string> ToStringValueDictionary(this object obj)
        {
            return ObjectToDictionary<string>(obj);
        }

        public static IDictionary<string, object> ToObjectValueDictionary(this object obj)
        {
            return ObjectToDictionary<object>(obj);
        }

        private static IDictionary<string, TValue> ObjectToDictionary<TValue>(object obj)
        {
            var objAsStringValueDictionary = obj as IDictionary<string, TValue>;
            if (objAsStringValueDictionary != null)
            {
                return objAsStringValueDictionary;
            }

            var result = new RouteValueDictionary(obj).AsEnumerable();
            var typeOfValue = typeof(TValue);
            if (typeOfValue != typeof(object))
            {
                result = result.Where(i => Reflection.AreAssignable(typeof(TValue), i.Value?.GetType()));
            }

            return result.ToDictionary(i => i.Key.Replace('_', '-'), i => (TValue)i.Value);
        }
    }
}
