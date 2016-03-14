namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid action return type when expecting IActionResult.
    /// </summary>
    public class ActionResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ActionResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ActionResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
