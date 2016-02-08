namespace MyTested.Mvc.Utilities.Extensions
{
    using Utilities;

    /// <summary>
    /// Provides extension methods to Object class.
    /// </summary>
    public static class ObjectExtensions
    {
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
    }
}
