namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using Exceptions;
    using Extensions;

    /// <summary>
    /// Validator class containing invocation validation logic.
    /// </summary>
    public static class InvocationValidator
    {
        /// <summary>
        /// Validated whether a non-null exception is provided and throws <see cref="InvocationAssertionException"/> with proper message.
        /// </summary>
        /// <param name="exception">Exception to be validated.</param>
        /// <param name="exceptionMessagePrefix">Prefix to put in front of the exception message.</param>
        public static void CheckForException(Exception exception, string exceptionMessagePrefix)
        {
            if (exception != null)
            {
                var message = FormatExceptionMessage(exception.Message);

                if (exception is AggregateException exceptionAsAggregateException)
                {
                    var innerExceptions = exceptionAsAggregateException
                        .InnerExceptions
                        .Select(ex => $"{ex.GetName()}{FormatExceptionMessage(ex.Message)}");

                    message = $" (containing {string.Join(", ", innerExceptions)})";
                }

                // ArgumentOutOfRangeException may be thrown because of missing route values
                if (exception is ArgumentOutOfRangeException exceptionAsArgumentOutOfRangeException 
                    && exceptionAsArgumentOutOfRangeException.StackTrace.Contains("Microsoft.AspNetCore.Mvc.Routing"))
                {
                    throw new InvalidOperationException("Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
                }
                
                throw new InvocationAssertionException(string.Format(
                    "{0} no exception but {1}{2} was thrown without being caught.",
                    exceptionMessagePrefix,
                    exception.GetType().ToFriendlyTypeName(),
                    message));
            }
        }

        public static void CheckForNullException(Exception exception, string exceptionMessagePrefix)
        {
            if (exception == null)
            {
                throw new InvocationAssertionException(
                    $"{exceptionMessagePrefix} exception to be thrown, but in fact none was caught.");
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
