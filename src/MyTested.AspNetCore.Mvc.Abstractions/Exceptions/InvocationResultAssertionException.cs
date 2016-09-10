namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid action return type.
    /// </summary>
    public class InvocationResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvocationResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
