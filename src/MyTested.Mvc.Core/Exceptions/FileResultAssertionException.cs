namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>, <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>, <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>.
    /// </summary>
    public class FileResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
