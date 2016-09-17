namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>, <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>/>.
    /// </summary>
    public class CreatedResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CreatedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
