namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using Utilities.Extensions;

    /// <summary>
    /// Validator class containing common validation logic.
    /// </summary>
    public static class CommonValidator
    {
        /// <summary>
        /// Validates object for null reference.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNullReference(
            object value,
            string errorMessageName = "Value")
        {
            if (value == null)
            {
                throw new NullReferenceException($"{errorMessageName} cannot be null.");
            }
        }

        /// <summary>
        /// Validates string for null reference or whitespace.
        /// </summary>
        /// <param name="value">String to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNotWhiteSpaceString(
            string value,
            string errorMessageName = "Value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullReferenceException($"{errorMessageName} cannot be null or white space.");
            }
        }

        /// <summary>
        /// Validates whether the provided value is not null or equal to the type's default value.
        /// </summary>
        /// <typeparam name="T">Type of the provided value.</typeparam>
        /// <param name="value">Value to be validated.</param>
        /// <param name="errorMessage">Error message if the validation fails.</param>
        public static void CheckForEqualityWithDefaultValue<T>(T value, string errorMessage)
        {
            if (CheckForDefaultValue(value))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        /// <summary>
        /// Validated whether a non-null exception is provided and throws ActionCallAssertionException with proper message.
        /// </summary>
        /// <param name="exception">Exception to be validated.</param>
        public static void CheckForException(Exception exception)
        {
            if (exception != null)
            {
                var messageFormat = "{0}{1} was thrown but was not caught or expected.";
                var message = FormatExceptionMessage(exception.Message);

                var exceptionAsAggregateException = exception as AggregateException;
                if (exceptionAsAggregateException != null)
                {
                    var innerExceptions = exceptionAsAggregateException
                        .InnerExceptions
                        .Select(ex => $"{ex.GetName()}{FormatExceptionMessage(ex.Message)}");

                    message = $" (containing {string.Join(", ", innerExceptions)})";
                }

                // ArgumentOutOfRangeException may be thrown because of missing route values
                var exceptionAsArgumentOutOfRangeException = exception as ArgumentOutOfRangeException;
                if (exceptionAsArgumentOutOfRangeException != null)
                {
                    messageFormat = "{0} was thrown but was not caught or expected. One possible reason my be unresolved route values. Consider calling 'WithResolvedRouteValues' on the controller builder or provide HTTP request path by using 'WithHttpRequest'.";
                }
                
                throw new ActionCallAssertionException(string.Format(
                    messageFormat,
                    exception.GetType().ToFriendlyTypeName(),
                    message));
            }
        }

        /// <summary>
        /// Validates that two objects are equal using the Equals method.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <returns>True or false.</returns>
        public static bool CheckEquality<T>(T expected, T actual)
        {
            return expected.Equals(actual);
        }

        /// <summary>
        /// Validates whether object equals the default value for its type.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="value">Object to test.</param>
        /// <returns>True or false.</returns>
        public static bool CheckForDefaultValue<TValue>(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(value, default(TValue));
        }

        /// <summary>
        /// Validates whether type can be null.
        /// </summary>
        /// <param name="type">Type to check.</param>
        public static void CheckIfTypeCanBeNull(Type type)
        {
            bool canBeNull = !type.GetTypeInfo().IsValueType || (Nullable.GetUnderlyingType(type) != null);
            if (!canBeNull)
            {
                throw new ActionCallAssertionException($"{type.ToFriendlyTypeName()} cannot be null.");
            }
        }

        private static string FormatExceptionMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message)
                 ? string.Empty
                 : $" with '{message}' message";
        }
    }
}
