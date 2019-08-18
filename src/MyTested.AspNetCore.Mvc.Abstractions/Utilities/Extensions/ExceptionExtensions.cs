namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Linq;

    public static class ExceptionExtensions
    {
        public static Exception Unwrap(this Exception exception)
        {
            if (exception is AggregateException aggregateException)
            {
                return aggregateException.InnerExceptions.FirstOrDefault();
            }

            return exception;
        }
    }
}
