namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>.
    /// </summary>
    public class JsonResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JsonResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
