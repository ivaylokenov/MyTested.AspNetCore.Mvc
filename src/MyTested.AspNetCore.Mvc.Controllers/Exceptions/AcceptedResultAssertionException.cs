namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/>/>.
    /// </summary>
    public class AcceptedResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptedResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AcceptedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
