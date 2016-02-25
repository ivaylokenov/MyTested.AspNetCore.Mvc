namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid data assertion.
    /// </summary>
    public class DataProviderAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DataProviderAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public DataProviderAssertionException(string message)
            : base(message)
        {
        }
    }
}
