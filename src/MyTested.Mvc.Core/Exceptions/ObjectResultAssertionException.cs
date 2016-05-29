namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
    /// </summary>
    public class ObjectResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ObjectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
