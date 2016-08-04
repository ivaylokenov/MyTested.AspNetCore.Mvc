namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Linq;

    public static class ExceptionExtensions
    {
        public static Exception Unwrap(this Exception exception)
        {
            var aggregateException = exception as AggregateException;
            if (aggregateException != null)
            {
                return aggregateException.InnerExceptions.FirstOrDefault();
            }

            return exception;
        }
    }
}
