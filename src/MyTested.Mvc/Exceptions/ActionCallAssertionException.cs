namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid action call.
    /// </summary>
    public class ActionCallAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ActionCallAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ActionCallAssertionException(string message)
            : base(message)
        {
        }
    }
}
