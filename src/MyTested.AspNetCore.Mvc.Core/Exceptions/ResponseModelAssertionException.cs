namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid action return type when expecting response model.
    /// </summary>
    public class ResponseModelAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResponseModelAssertionException(string message)
            : base(message)
        {
        }
    }
}
