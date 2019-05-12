namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ConflictResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.ConflictObjectResult"/>.
    /// </summary>
    public class ConflictResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConflictResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
