namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid JSON result.
    /// </summary>
    public class JsonResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the JSONResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public JsonResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
