namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid action call.
    /// </summary>
    public class ActionCallAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCallAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ActionCallAssertionException(string message)
            : base(message)
        {
        }
    }
}
