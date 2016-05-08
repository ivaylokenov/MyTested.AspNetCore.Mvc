namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>, <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
    /// </summary>
    public class ViewResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ViewResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
