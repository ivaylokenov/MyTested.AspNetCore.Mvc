namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Http.HttpRequest"/>.
    /// </summary>
    public class InvalidHttpRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHttpRequestException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidHttpRequestException(string message)
            : base(message)
        {
        }
    }
}
