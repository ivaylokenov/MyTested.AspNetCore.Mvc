namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using Exceptions;
    using Extensions;

    /// <summary>
    /// Validator class containing common validation logic.
    /// </summary>
    public static class ActionValidator
    {
        /// <summary>
        /// Validated whether a non-null exception is provided and throws ActionCallAssertionException with proper message.
        /// </summary>
        /// <param name="exception">Exception to be validated.</param>
        public static void CheckForException(Exception exception)
        {
            if (exception != null)
            {
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
                if (exceptionAsArgumentOutOfRangeException != null && exceptionAsArgumentOutOfRangeException.StackTrace.Contains("Microsoft.AspNetCore.Mvc.Routing"))
                {
                    throw new InvalidOperationException("Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
                }
                
                throw new ActionCallAssertionException(string.Format(
                    "{0}{1} was thrown but was not caught or expected.",
                    exception.GetType().ToFriendlyTypeName(),
                    message));
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
