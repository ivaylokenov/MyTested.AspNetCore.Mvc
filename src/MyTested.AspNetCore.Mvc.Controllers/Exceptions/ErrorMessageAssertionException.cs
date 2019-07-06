namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid error messages.
    /// </summary>
    public class ErrorMessageAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessageAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ErrorMessageAssertionException(string message)
            : base(message)
        {
        }
    }
}
