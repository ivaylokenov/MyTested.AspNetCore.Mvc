namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid action call.
    /// </summary>
    public class InvocationAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvocationAssertionException(string message)
            : base(message)
        {
        }
    }
}
