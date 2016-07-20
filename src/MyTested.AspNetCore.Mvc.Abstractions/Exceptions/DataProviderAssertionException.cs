namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid data assertion.
    /// </summary>
    public class DataProviderAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataProviderAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DataProviderAssertionException(string message)
            : base(message)
        {
        }
    }
}
