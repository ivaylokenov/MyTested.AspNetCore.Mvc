namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
    /// </summary>
    public class RedirectResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RedirectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
