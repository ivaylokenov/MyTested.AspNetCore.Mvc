namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid action return type when expecting IHttpActionResult.
    /// </summary>
    public class ActionResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpActionResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ActionResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
