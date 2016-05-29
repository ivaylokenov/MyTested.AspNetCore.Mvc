namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid action return type.
    /// </summary>
    public class ActionResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ActionResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
