namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid error <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    public class UnprocessableEntityResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnprocessableEntityResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
