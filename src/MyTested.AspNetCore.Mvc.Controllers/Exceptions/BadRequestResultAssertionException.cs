namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
    /// </summary>
    public class BadRequestResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BadRequestResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
